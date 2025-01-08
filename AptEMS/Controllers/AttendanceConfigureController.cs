using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;


namespace AptEMS.Controllers
{
    public class AttendanceConfigureController : Controller
    {

        DAL.AttendanceConfigure objdalemp = new DAL.AttendanceConfigure();

        public ActionResult Index()
        {

            List<Models.AttendanceConfigure> ie = objdalemp.GetAttendanceConfigure();
            return View(ie);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.AttendanceConfigure e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.AddAttendanceConfigure(e1);
                if (i == 1)
                {
                    TempData["SuccessMessage"] = "AttendanceConfigure Added Successfully";
                    return RedirectToAction("Index");
                }
                else if (i == -1)
                {
                    // Handle duplicate ID case
                    //ViewBag.ErrorMessage = "This ID already exists.";

                    ModelState.AddModelError("Empid", "This Created By already exists.");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            Models.AttendanceConfigure e1 = new Models.AttendanceConfigure();
            e1.CreatedBy = id;
            int i = objdalemp.DeleteAttendanceConfigure(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Models.AttendanceConfigure e1 = new Models.AttendanceConfigure();
            e1.CreatedBy = id;
            e1 = objdalemp.SearchAttendanceConfigure(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.AttendanceConfigure e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateAttendanceConfigure(e1);
                if (i == 1)
                {
                    TempData["SuccessMessage"] = "Attendance Configure Saved Successfully";
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }
    }
}