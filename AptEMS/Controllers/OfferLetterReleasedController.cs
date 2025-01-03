using AptEMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AptEMS.Controllers
{
    public class OfferLetterReleasedController : Controller
    {
        DAL.OfferLetterReleased objdalemp = new DAL.OfferLetterReleased();
        public ActionResult Index()
        {

            List<Models.OfferLetterReleased> ie = objdalemp.GetOfferLetterReleased();
            return View(ie);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.OfferLetterReleased e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.AddOfferLetterReleased(e1);
                if (i == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (i == -1)
                {
                    // Handle duplicate ID case
                    //ViewBag.ErrorMessage = "This ID already exists.";

                    ModelState.AddModelError("Email", "This Email already exists.");
                }
            }
            return View();
        }


        [HttpGet]


        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                // Handle the case where id is null or empty, such as returning an error message or redirecting.
                return RedirectToAction("Index"); // Example: redirecting to index or showing an error view.
            }

            Models.OfferLetterReleased e1 = new Models.OfferLetterReleased();
            e1.Mobile = id; // No need to use 'Value' since id is already a string.
            int i = objdalemp.DeleteOfferLetterReleased(e1);
            if (i == 1)
            {
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            Models.OfferLetterReleased e1 = new Models.OfferLetterReleased();
            e1.Mobile = id;
            e1 = objdalemp.SearchOfferLetterReleased(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.OfferLetterReleased e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateOfferLetterReleased(e1);
                if (i == 1)
                {
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }




        [HttpGet]
        public ActionResult Setasemployee()
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
        public ActionResult Setasemployee(Models.Employee e1, HttpPostedFileBase EmpphotoFile, HttpPostedFileBase PANphotoFile, HttpPostedFileBase AadharphotoFile)
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
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "OfferLetterReleased");
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


    }
}