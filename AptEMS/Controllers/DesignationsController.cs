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
    public class DesignationsController : Controller
    {
        private dbAptResourceEntities2 db = new dbAptResourceEntities2();

        // GET: Designations
        public async Task<ActionResult> Index()
        {
            return View(await db.Designations.ToListAsync());
        }

        // GET: Designations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = await db.Designations.FindAsync(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // GET: Designations/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Designation1,Created")] Designation designation)
        {
            if (ModelState.IsValid)
            {
                // Check if a designation with the same ID already exists
                var existingDesignation = await db.Designations.FindAsync(designation.ID);
                if (existingDesignation != null)
                {
                    ModelState.AddModelError("ID", "This ID already exists.");
                    return View(designation); // Return view with error
                }

                // If no duplicate, add and save the new designation
                db.Designations.Add(designation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(designation);
        }


        // GET: Designations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = await db.Designations.FindAsync(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Designation1,Created")] Designation designation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(designation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(designation);
        }

        // GET: Designations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = await db.Designations.FindAsync(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Designation designation = await db.Designations.FindAsync(id);
            db.Designations.Remove(designation);
            await db.SaveChangesAsync();
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
