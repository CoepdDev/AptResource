using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class EmployeeController : Controller
    {
        DAL.Employee objdalemp = new DAL.Employee();
        public ActionResult Index()
        {
            List<Models.Employee> ie = objdalemp.Getemp();
            return View(ie);
        }
   

        [HttpGet]
        public ActionResult Create()
        {

            dbAptResourceEntities2 db = new dbAptResourceEntities2();

      


            // Check if Designations table has data
            if (db.Designations.Any())
            {
                ViewBag.Designation = new SelectList(db.Designations, "Designation1", "Designation1");
            }
            else
            {
                // Handle the case where Designations is empty or null
                ViewBag.Designation = new SelectList(Enumerable.Empty<SelectListItem>());
            }


            return View();



        }
        [HttpPost]
        public ActionResult Create(Models.Employee e1, HttpPostedFileBase EmpphotoFile, HttpPostedFileBase PANphotoFile, HttpPostedFileBase AadharphotoFile)
        {


            dbAptResourceEntities2 db = new dbAptResourceEntities2();

            if (db.Designations.Any())
            {
                ViewBag.Designation = new SelectList(db.Designations, "Designation1", "Designation1");
            }
            else
            {
                ViewBag.Designation = new SelectList(Enumerable.Empty<SelectListItem>());
            }


            if (ModelState.IsValid)
            {
                string uploadPath = Server.MapPath("~/EMPfiles");


                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }


                if (EmpphotoFile != null && EmpphotoFile.ContentLength > 0)
                {
                    string empphotoPath = Server.MapPath("~/EMPfiles");
                    string empphotoFileName = Guid.NewGuid() + Path.GetExtension(EmpphotoFile.FileName);
                    EmpphotoFile.SaveAs(Path.Combine(empphotoPath, empphotoFileName));

                    e1.Empphoto = "/EMPfiles/" + empphotoFileName;






                }

            
                if (PANphotoFile != null && PANphotoFile.ContentLength > 0)
                {
                    string panphotoPath = Server.MapPath("~/EMPfiles");
                    string panphotoFileName = Guid.NewGuid() + Path.GetExtension(PANphotoFile.FileName);
                    PANphotoFile.SaveAs(Path.Combine(panphotoPath, panphotoFileName));
                    e1.PANphoto = "/EMPfiles/" + panphotoFileName;



                }

                if (AadharphotoFile != null && AadharphotoFile.ContentLength > 0)
                {
                    string aadharphotoPath = Server.MapPath("~/EMPfiles");
                    string aadharphotoFileName = Guid.NewGuid() + Path.GetExtension(AadharphotoFile.FileName);

                    AadharphotoFile.SaveAs(Path.Combine(aadharphotoPath, aadharphotoFileName));

                    e1.Aadharphoto = "/EMPfiles/" + aadharphotoFileName;




                }


             

                int i = objdalemp.Addemp(e1);
                 if (i == 1)
                 {

                    TempData["SuccessMessage"] = "New Employee is Added";
                    return RedirectToAction("Index");
                 }

                else if (i == -1)
                {
                    // Handle duplicate ID case
                    //ViewBag.ErrorMessage = "This ID already exists.";

                    ModelState.AddModelError("Id", "This ID already exists.");
                }                                                                                                                                                                                                                                                                                   





            }
            return View(e1);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Models.Employee e1 = new Models.Employee();
            e1.Id = id;
            int i = objdalemp.Deleteemp(e1);
            if (i == 1)                                                                                     
            {
                return RedirectToAction("index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            
            dbAptResourceEntities2 db = new dbAptResourceEntities2();

            if (db.Designations.Any())
            {
                ViewBag.Designation = new SelectList(db.Designations, "Designation1", "Designation1");
            }
            else
            {
                ViewBag.Designation = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            Models.Employee e1 = new Models.Employee();
            e1.Id = id;
            e1 = objdalemp.Searchemp(e1);

  




      

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.Employee e1)
        {
            dbAptResourceEntities2 db = new dbAptResourceEntities2();

            if (db.Designations.Any())
            {
                ViewBag.Designation = new SelectList(db.Designations, "Designation1", "Designation1");
            }
            else
            {
                ViewBag.Designation = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            if (ModelState.IsValid)
            {
                int i = objdalemp.Updateemp(e1);
                if (i == 1)
                {
                    TempData["SuccessMessage"] = "Changes Saved Successfully";
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            // Create an instance of the Shiftreg model to hold the result
            Models.Employee e1 = new Models.Employee();

            // Set the s_type to the provided id (shift type)
            e1.Id = id;

            // Use the Searchshiftreg method in DAL to get shift details
            e1 = objdalemp.Searchemp(e1);

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

