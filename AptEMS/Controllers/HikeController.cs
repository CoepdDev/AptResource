using AptEMS.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace AptEMS.Controllers
{
    public class HikeController : Controller
    {
        DAL.Hike objdalemp = new DAL.Hike();
        public ActionResult Index()
        {
            List<Models.Hike> ie = objdalemp.GetHike();
            return View(ie);
        }

        [HttpGet]
      

   
        public ActionResult Create()
        {
            using (AptEmsContext db = new AptEmsContext())
            {
                // Fetch only active employees
                var employees = db.Database.SqlQuery<Employee>(
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
        public ActionResult Create(Models.Hike e1)
        {

            using (AptEmsContext db = new AptEmsContext())
            {
                // Fetch only active employees
                var employees = db.Database.SqlQuery<Employee>(
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
          
             

            


                int i = objdalemp.AddHike(e1);
                if (i == 1)
                {
                    TempData["SuccessMessage"] = "Hike Added Successfully";
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
            Models.Hike e1 = new Models.Hike();
            e1.Empid = id;
            int i = objdalemp.DeleteHike(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Models.Hike e1 = new Models.Hike();
            e1.Empid = id;
            e1 = objdalemp.SearchHike(e1);


          

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.Hike e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateHike(e1);
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
                var employeeDetails = db.Database.SqlQuery<Hike>(
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