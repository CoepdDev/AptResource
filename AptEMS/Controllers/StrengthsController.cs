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
    public class StrengthsController : Controller
    {
        private AptEmsContext db = new AptEmsContext();

        // GET: Strengths
        public async Task<ActionResult> Index()
        {
            return View(await db.Strengths.ToListAsync());
        }

        
        // GET: Strengths/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StrengthID,StrengthName,IsActive")] Strength strength)
        {
            if (ModelState.IsValid)
            {
                db.Strengths.Add(strength);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(strength);
        }

        // GET: Strengths/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Strength strength = await db.Strengths.FindAsync(id);
            if (strength == null)
            {
                return HttpNotFound();
            }
            return View(strength);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StrengthID,StrengthName,IsActive")] Strength strength)
        {
            if (ModelState.IsValid)
            {
                db.Entry(strength).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(strength);
        }

        // POST: Strength/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var strength = await db.Strengths.FindAsync(id);
            if (strength != null)
            {
                db.Strengths.Remove(strength);
                await db.SaveChangesAsync();
            }
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
