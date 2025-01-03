using AptEMS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AptEMS.Controllers
{
    public class DailyAttendanceController : Controller
    {

        DAL.DailyAttendance objdalemp = new DAL.DailyAttendance();
        public ActionResult Index()
        {
            List<Models.DailyAttendance> ie = objdalemp.GetDailyAttendance();
            return View(ie);
        }

        [HttpGet]



        public ActionResult Create()
        {
            


            return View();
        }



        [HttpPost]
        public ActionResult Create(Models.DailyAttendance e1)
        {

            if (ModelState.IsValid)
            {
                int i = objdalemp.AddDailyAttendance(e1);
                if (i == 1)
                {
                    TempData["SuccessMessage"] = "Attendance Added Successfully";
                    return RedirectToAction("Index");
                }
                else if (i == -1)
                {
                    // Handle duplicate ID case
                    //ViewBag.ErrorMessage = "This ID already exists.";

                    ModelState.AddModelError("Empid", "This ID already exists.");
                }

            }
            return View(e1);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Models.DailyAttendance e1 = new Models.DailyAttendance();
            e1.Empid = id;
            int i = objdalemp.DeleteDailyAttendance(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Models.DailyAttendance e1 = new Models.DailyAttendance();
            e1.Empid = id;
            e1 = objdalemp.SearchDailyAttendance(e1);




            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.DailyAttendance e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateDailyAttendance(e1);
                if (i == 1)
                {
                    TempData["SuccessMessage"] = "Hike Saved Successfully";
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }


        public JsonResult GetDetailsByEmpid(int empid)
        {
            using (AptEmsContext db = new AptEmsContext())
            {
                var employeeDetails = db.Database.SqlQuery<DailyAttendance>(
                    "SELECT FirstName, Designation FROM Employee WHERE Id = @p0", empid).FirstOrDefault();

                if (employeeDetails != null)
                {
                    return Json(new { success = true, employeeDetails }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Employee not found." }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}