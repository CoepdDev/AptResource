using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
namespace AptEMS.Controllers
{
    public class RejectedController : Controller
    {
        DAL.Rejected objdalemp = new DAL.Rejected();
        public ActionResult Index()
        {

            List<Models.Rejected> ie = objdalemp.GetRejected();
            return View(ie);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Rejected e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.AddRejected(e1);
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

            Models.Rejected e1 = new Models.Rejected();
            e1.Mobile = id; // No need to use 'Value' since id is already a string.
            int i = objdalemp.DeleteRejected(e1);
            if (i == 1)
            {
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            Models.Rejected e1 = new Models.Rejected();
            e1.Mobile = id;
            e1 = objdalemp.SearchRejected(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.Rejected e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateRejected(e1);
                if (i == 1)
                {
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }
    }
}