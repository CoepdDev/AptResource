using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;


namespace AptEMS.Controllers
{
    public class LoanController : Controller
    {
        DAL.Loan objdalemp = new DAL.Loan();
        public ActionResult Index()
        {
            List<Models.Loan> ie = objdalemp.GetLoan();
            return View(ie);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Loan e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.AddLoan(e1);
                if (i == 1)
                {
                    TempData["SuccessMessage"] = "Loan Added Successfully";
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
            Models.Loan e1 = new Models.Loan();
            e1.Empid = id;
            int i = objdalemp.DeleteLoan(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Models.Loan e1 = new Models.Loan();
            e1.Empid = id;
            e1 = objdalemp.SearchLoan(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.Loan e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateLoan(e1);
                if (i == 1)
                {
                    TempData["SuccessMessage"] = "Changes Saved Successfully";
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }

        public JsonResult GetDetailsByEmpid(int empid)
        {
            using (AptEmsContext db = new AptEmsContext())
            {
                var employeeDetails = db.Database.SqlQuery<AptEMS.Models.Loan>(
                    "SELECT FirstName FROM Employee WHERE Id = @p0", empid).FirstOrDefault();

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