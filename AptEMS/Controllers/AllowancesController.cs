﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class AllowancesController : Controller
    {
        DAL.Allowances objdalemp = new DAL.Allowances();

        public ActionResult Index()
        {

            List<Models.Allowances> ie = objdalemp.Get();
            return View(ie);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Allowances e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.Add(e1);
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
            Models.Allowances e1 = new Models.Allowances();
            e1.Empid = id;
            int i = objdalemp.Delete(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Models.Allowances e1 = new Models.Allowances();
            e1.Empid = id;
            e1 = objdalemp.Search(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.Allowances e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.Update(e1);
                if (i == 1)
                {
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }
    }
}