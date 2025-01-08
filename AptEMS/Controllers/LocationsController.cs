using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class LocationsController : Controller
    {
        private AptEmsContext db = new AptEmsContext();

        // GET: Locations
        public async Task<ActionResult> Index()
        {
            return View(await db.Locations.ToListAsync());
        }

        

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LocationID,LocationName,IsActive")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = await db.Locations.FindAsync(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LocationID,LocationName,IsActive")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var location = await db.Locations.FindAsync(id);
            if (location == null)
            {
                TempData["ErrorMessage"] = "Location not found.";
                return RedirectToAction("Index");
            }

            db.Locations.Remove(location);
            await db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Location deleted successfully.";
            return RedirectToAction("Index");
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
