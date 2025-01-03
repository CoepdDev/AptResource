using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AptEMS.Controllers
{
    public class SecondRoundController : Controller
    {
        DAL.SecondRound objdalemp = new DAL.SecondRound();
        public ActionResult Index()
        {

            List<Models.SecondRound> ie = objdalemp.GetSecondRound();
            return View(ie);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.SecondRound e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.AddSecondRound(e1);
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
        //public ActionResult Delete(int id)
        //{
        //    Models.SecondRound e1 = new Models.SecondRound();
        //    e1.Mobile = id;
        //    int i = objdalemp.DeleteSecondRound(e1);
        //    if (i == 1)
        //    {
        //        return RedirectToAction("index");
        //    }

        //    return View();
        //}

        //public ActionResult Deleten(string id)
        //{
        //    if (!id.HasValue)
        //    {
        //        // Handle the case where id is null, such as returning an error message or redirecting.
        //        return RedirectToAction("Index"); // Example: redirecting to index or showing an error view.
        //    }

        //    Models.SecondRound e1 = new Models.SecondRound();
        //    e1.Mobile = id.Value; // Safely access the value since we checked for null
        //    int i = objdalemp.DeleteSecondRound(e1);
        //    if (i == 1)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}

        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                // Handle the case where id is null or empty, such as returning an error message or redirecting.
                return RedirectToAction("Index"); // Example: redirecting to index or showing an error view.
            }

            Models.SecondRound e1 = new Models.SecondRound();
            e1.Mobile = id; // No need to use 'Value' since id is already a string.
            int i = objdalemp.DeleteSecondRound(e1);
            if (i == 1)
            {
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            Models.SecondRound e1 = new Models.SecondRound();
            e1.Mobile = id;
            e1 = objdalemp.SearchSecondRound(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.SecondRound e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateSecondRound(e1);
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
            Models.SecondRound e1 = new Models.SecondRound();
            e1.Mobile = id;
            e1 = objdalemp.SearchSecondRound(e1);


            Models.ThirdRound ThirdRound = new Models.ThirdRound
            {
                Name = e1.Name,
                Role = e1.Role,
                Mobile = e1.Mobile,
                Email = e1.Email,
            };
            return View(e1);
        }
        //[HttpPost]
        //public ActionResult NextRound(Models.ThirdRound e1)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int i = objdalemp.AddNextRound(e1);
        //        if (i == 1)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View();
        //}


        [HttpPost]
        public ActionResult NextRound(Models.ThirdRound e1)
        {
            if (ModelState.IsValid)
            {
                // Insert into the SecondRound table
                int insertResult = objdalemp.AddNextRound(e1);

                if (insertResult == 1)
                {
                    // After successful insertion, delete from the FirstRound table
                    Models.SecondRound secondRoundRecord = new Models.SecondRound
                    {
                        Mobile = e1.Mobile
                    };

                    int deleteResult = objdalemp.DeleteSecondRound(secondRoundRecord);

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