using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;
namespace AptEMS.Controllers
{
    public class ThirdRoundController : Controller
    {

        DAL.ThirdRound objdalemp = new DAL.ThirdRound();
        //DAL.SecondRound objdalemp = new DAL.SecondRound();
        public ActionResult Index()
        {

            List<Models.ThirdRound> ie = objdalemp.GetThirdRound();
            return View(ie);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.ThirdRound e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.AddThirdRound(e1);
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
            Models.ThirdRound e1 = new Models.ThirdRound();
            e1.Mobile = id;
            int i = objdalemp.DeleteThirdRound(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            Models.ThirdRound e1 = new Models.ThirdRound();
            e1.Mobile = id;
            e1 = objdalemp.SearchThirdRound(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.ThirdRound e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateThirdRound(e1);
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
            Models.ThirdRound e1 = new Models.ThirdRound();
            e1.Mobile = id;
            e1 = objdalemp.SearchThirdRound(e1);


            Models.SelectedCandidates ThirdRound = new Models.SelectedCandidates
            {
                Name = e1.Name,
                Role = e1.Role,
                Mobile = e1.Mobile,
                Email= e1.Email,
            };
            return View(e1);
        }
       

        [HttpPost]
        public ActionResult NextRound(Models.SelectedCandidates e1)
        {
            if (ModelState.IsValid)
            {
                // Insert into the SecondRound table
                int insertResult = objdalemp.AddNextRound(e1);

                if (insertResult == 1)
                {
                    // After successful insertion, delete from the FirstRound table
                    Models.ThirdRound thirdRoundRecord = new Models.ThirdRound
                    {
                        Mobile = e1.Mobile
                    };

                    int deleteResult = objdalemp.DeleteThirdRound(thirdRoundRecord);

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