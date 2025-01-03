using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class FirstRoundController : Controller
    {
        DAL.FirstRound objdalemp = new DAL.FirstRound();

        public ActionResult Index()
        {

            List<Models.FirstRound> ie = objdalemp.GetFirstRound();
            return View(ie);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.FirstRound e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.AddFirstRound(e1);
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
            Models.FirstRound e1 = new Models.FirstRound();
            e1.Mobile = id;
            int i = objdalemp.DeleteFirstRound(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            Models.FirstRound e1 = new Models.FirstRound();
            e1.Mobile = id;
            e1 = objdalemp.SearchFirstRound(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.FirstRound e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateFirstRound(e1);
                if (i == 1)
                {
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }




        [HttpGet]
        public ActionResult NextRound(string id)
        {

            Models.FirstRound e1 = new Models.FirstRound();
            e1.Mobile = id;
            e1 = objdalemp.SearchFirstRound(e1);


            Models.SecondRound secondRound = new Models.SecondRound
            {
                Name = e1.Name,
                Role = e1.Role,
                Mobile = e1.Mobile,
                Email = e1.Email,
            };



            // Pass the fetched data to the view
            return View(e1);
        }







        [HttpPost]
        public ActionResult NextRound(Models.SecondRound e1)
        {
            if (ModelState.IsValid)
            {

                int insertResult = objdalemp.AddNextRound(e1);

                if (insertResult == 1)
                {
                    // After successful insertion, delete from the FirstRound table
                    Models.FirstRound firstRoundRecord = new Models.FirstRound
                    {
                        Mobile = e1.Mobile
                    };

                    int deleteResult = objdalemp.DeleteFirstRound(firstRoundRecord);

                    if (deleteResult == 1)
                    {
                        // Redirect to Index if both insertion and deletion were successful
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Handle deletion failure (optional)
                        ModelState.AddModelError("", "Failed to delete the record from FirstRound.");
                    }
                }
                else
                {
                    // Handle insertion failure (optional)
                    ModelState.AddModelError("", "Failed to insert the record into SecondRound.");
                }
            }

            // If we reach here, something went wrong, re-render the view
            return View(e1);
        }







        [HttpGet]
        public ActionResult Rejected(string id)
        {
            Models.FirstRound e1 = new Models.FirstRound();
            e1.Mobile = id;
            e1 = objdalemp.SearchFirstRound(e1);

            Models.Rejected rejected = new Models.Rejected
            {
                Name = e1.Name,
                Role = e1.Role,
                Mobile = e1.Mobile,
                Email = e1.Email,
            };

            // Pass the correct object to the view
            return View(rejected);
        }








        [HttpPost]
        public ActionResult Rejected(Models.Rejected e1)
        {
            if (ModelState.IsValid)
            {
                // Insert into the SecondRound table
                int insertResult = objdalemp.AddRejected(e1);

                if (insertResult == 1)
                {
                    // After successful insertion, delete from the FirstRound table
                    Models.FirstRound firstRoundRecord = new Models.FirstRound
                    {
                        Mobile = e1.Mobile
                    };

                    int deleteResult = objdalemp.DeleteFirstRound(firstRoundRecord);

                    if (deleteResult == 1)
                    {
                        // Redirect to Index if both insertion and deletion were successful
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Handle deletion failure (optional)
                        ModelState.AddModelError("", "Failed to delete the record from FirstRound.");
                    }
                }
                else
                {
                    // Handle insertion failure (optional)
                    ModelState.AddModelError("", "Failed to insert the record into SecondRound.");
                }
            }

            // If we reach here, something went wrong, re-render the view
            return View(e1);
        }

    }


}



