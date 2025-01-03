using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class StatisticController : Controller
    {
        private AptEmsContext db = new AptEmsContext();

        // GET: Statistic
        public ActionResult Index()
        {
            var statistics = db.Statistics.ToList();
            return View(statistics);
        }

        

        // GET: Statistic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Statistic/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IconType,Count,Label,IsActive")] Statistic statistic)
        {
            if (ModelState.IsValid)
            {
                db.Statistics.Add(statistic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statistic);
        }

        // GET: Statistic/Edit/5
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic statistic = db.Statistics.Find(Id);
            if (statistic == null)
            {
                return HttpNotFound();
            }
            return View(statistic);
        }

        // POST: Statistic/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IconType,Count,Label,IsActive")] Statistic statistic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statistic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statistic);
        }

        // DELETE: Statistic/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            Statistic statistic = db.Statistics.Find(Id);
            if (statistic != null)
            {
                db.Statistics.Remove(statistic);
                db.SaveChanges();
            }
            return RedirectToAction("Index"); // This will reload the index page after delete
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
