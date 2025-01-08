using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class ShiftController : Controller
    {
        DAL.shift objdalemp = new DAL.shift();




        public ActionResult Index()
        {

            List<Models.shift> ie = objdalemp.Getshift();
            return View(ie);

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.shift e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.Addshift(e1);
                if (i == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (i == -1)
                {
                    // Handle duplicate ID case
                    //ViewBag.ErrorMessage = "This ID already exists.";

                    ModelState.AddModelError("sno", "This ID already exists.");
                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Models.shift e1 = new Models.shift();
            e1.sno = id;
            int i = objdalemp.Deleteshift(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Models.shift e1 = new Models.shift();
            e1.sno = id;
            e1 = objdalemp.Searchshift(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.shift e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.Updateshift(e1);
                if (i == 1)
                {
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }








    }
}