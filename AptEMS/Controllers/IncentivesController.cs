using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class IncentivesController : Controller
    {
        DAL.Incentives objdalemp = new DAL.Incentives();
        public ActionResult Index()
        {
            List<Models.Incentives> ie = objdalemp.GetIncentives();
            return View(ie);
        }

        [HttpGet]
        public ActionResult Create()
        {
            using (AptEmsContext db = new AptEmsContext())
            {
                // Fetch only active employees
                var employees = db.Database.SqlQuery<AptEMS.Models.Incentives>(
                    "SELECT * FROM Employee WHERE IsActive = 1").ToList();

                Debug.WriteLine("Active Employee Count: " + employees.Count);




                // Populate dropdown with active employees
                if (employees.Any())
                {
                    ViewBag.Employee = new SelectList(employees, "FirstName", "FirstName");
                }
                else
                {
                    // Empty dropdown if no active employees found
                    ViewBag.Employee = new SelectList(Enumerable.Empty<SelectListItem>());
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Incentives e1)
        {
            using (AptEmsContext db = new AptEmsContext())
            {
                // Fetch only active employees
                var employees = db.Database.SqlQuery<AptEMS.Models.Incentives>(
                    "SELECT * FROM Employee WHERE IsActive = 1").ToList();

                Debug.WriteLine("Active Employee Count: " + employees.Count);




                // Populate dropdown with active employees
                if (employees.Any())
                {
                    ViewBag.Employee = new SelectList(employees, "FirstName", "FirstName");
                }
                else
                {
                    // Empty dropdown if no active employees found
                    ViewBag.Employee = new SelectList(Enumerable.Empty<SelectListItem>());
                }
            }
            if (ModelState.IsValid)
            {
                int i = objdalemp.AddIncentives(e1);
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
            Models.Incentives e1 = new Models.Incentives();
            e1.Empid = id;
            int i = objdalemp.DeleteIncentives(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (AptEmsContext db = new AptEmsContext())
            {
                // Fetch only active employees
                var employees = db.Database.SqlQuery<AptEMS.Models.Incentives>(
                    "SELECT * FROM Employee WHERE IsActive = 1").ToList();

                Debug.WriteLine("Active Employee Count: " + employees.Count);




                // Populate dropdown with active employees
                if (employees.Any())
                {
                    ViewBag.Employee = new SelectList(employees, "FirstName", "FirstName");
                }
                else
                {
                    // Empty dropdown if no active employees found
                    ViewBag.Employee = new SelectList(Enumerable.Empty<SelectListItem>());
                }
            }
            Models.Incentives e1 = new Models.Incentives();
            e1.Empid = id;
            e1 = objdalemp.SearchIncentives(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.Incentives e1)
        {
            using (AptEmsContext db = new AptEmsContext())
            {
                // Fetch only active employees
                var employees = db.Database.SqlQuery<AptEMS.Models.Incentives>(
                    "SELECT * FROM Employee WHERE IsActive = 1").ToList();

                Debug.WriteLine("Active Employee Count: " + employees.Count);




                // Populate dropdown with active employees
                if (employees.Any())
                {
                    ViewBag.Employee = new SelectList(employees, "FirstName", "FirstName");
                }
                else
                {
                    // Empty dropdown if no active employees found
                    ViewBag.Employee = new SelectList(Enumerable.Empty<SelectListItem>());
                }
            }
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateIncentives(e1);
                if (i == 1)
                {
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }

        public JsonResult GetDetailsByEmpid(int empid)
        {
            using (AptEmsContext db = new AptEmsContext())
            {
                var employeeDetails = db.Database.SqlQuery<AptEMS.Models.Incentives>(
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