using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class LOPController : Controller
    {
        DAL.LOP objdalemp = new DAL.LOP();
        public ActionResult Index()
        {
            List<Models.LOP> ie = objdalemp.GetLOP();
            return View(ie);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.LOP e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.AddLOP(e1);
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
            Models.LOP e1 = new Models.LOP();
            e1.Empid = id;
            int i = objdalemp.DeleteLOP(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Models.LOP e1 = new Models.LOP();
            e1.Empid = id;
            e1 = objdalemp.SearchLOP(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.LOP e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateLOP(e1);
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
                var employeeDetails = db.Database.SqlQuery<AptEMS.Models.LOP>(
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