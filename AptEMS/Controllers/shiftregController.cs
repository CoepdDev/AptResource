using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;


namespace AptEMS.Controllers
{
    public class shiftregController : Controller
    {
        DAL.shiftreg objdalemp = new DAL.shiftreg();



        public ActionResult Index()
        {
            List<Models.Shiftreg> ie = objdalemp.Getshiftreg();
            return View(ie);

        }
        [HttpGet]
        public ActionResult Create()
        {
            dbAptResourceEntities1 db = new dbAptResourceEntities1();

            ViewBag.shifts = new SelectList(db.shifts, "shiftname", "shiftname");


            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Shiftreg e1)
        {

            if (ModelState.IsValid)
            {
                int i = objdalemp.Addshiftreg(e1);
                if (i == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (i == -1)
                {
                    // Handle duplicate ID case
                    //ViewBag.ErrorMessage = "This ID already exists.";

                    ModelState.AddModelError("s_type", "This ID already exists.");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            Models.Shiftreg e1 = new Models.Shiftreg();
            e1.s_type = id;
            int i = objdalemp.Deleteshiftreg(e1);
            if (i == 1)
            {
                return RedirectToAction("Index");
            }

            return View();

        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            dbAptResourceEntities1 db = new dbAptResourceEntities1();

            ViewBag.shifts = new SelectList(db.shifts, "shiftname", "shiftname");





            Models.Shiftreg e1 = new Models.Shiftreg();
            e1.s_type = id;
            e1 = objdalemp.Searchshiftreg(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.Shiftreg e1)
        {

            if (ModelState.IsValid)
            {

                int i = objdalemp.Updateshiftreg(e1);
                if (i == 1)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(e1);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            // Create an instance of the Shiftreg model to hold the result
            Models.Shiftreg e1 = new Models.Shiftreg();

            // Set the s_type to the provided id (shift type)
            e1.s_type = id;

            // Use the Searchshiftreg method in DAL to get shift details
            e1 = objdalemp.Searchshiftreg(e1);

            // Check if the shift record exists
            if (e1 == null)
            {
                return HttpNotFound(); // Return a 404 error if the record is not found
            }

            // Pass the model to the Details view
            return View(e1);
        }

    }
}