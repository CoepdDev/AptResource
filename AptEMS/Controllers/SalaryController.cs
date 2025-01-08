using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;


namespace AptEMS.Controllers
{
    public class SalaryController : Controller
    {
        DAL.Salary objdalemp = new DAL.Salary();
        public ActionResult Index()
        {
            List<Models.Salary> ie = objdalemp.GetSalary();
            return View(ie);
         
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Salary e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.AddSalary(e1);
                if (i == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (i == -1)
                {
                    // Handle duplicate ID case
                    //ViewBag.ErrorMessage = "This ID already exists.";

                    ModelState.AddModelError("Empid", "This ID already exists.");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Models.Salary e1 = new Models.Salary();
            e1.Empid = id;
            int i = objdalemp.DeleteSalary(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Models.Salary e1 = new Models.Salary();
            e1.Empid = id;
            e1 = objdalemp.SearchSalary(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.Salary e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateSalary(e1);
                if (i == 1)
                {
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }
    }
}