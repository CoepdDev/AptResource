using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class AdvancePaymnetController : Controller
    {
        DAL.AdvancePayment objdalemp = new DAL.AdvancePayment();

        public ActionResult Index()
        {

            List<Models.AdvancePayment> ie = objdalemp.Get();
            return View(ie);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.AdvancePayment e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.Add(e1);
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
            Models.AdvancePayment e1 = new Models.AdvancePayment();
            e1.Empid = id;
            int i = objdalemp.Delete(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Models.AdvancePayment e1 = new Models.AdvancePayment();
            e1.Empid = id;
            e1 = objdalemp.Search(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.AdvancePayment e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.Update(e1);
                if (i == 1)
                {
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }
    }
}