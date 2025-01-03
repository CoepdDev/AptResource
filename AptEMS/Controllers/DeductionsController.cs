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
    public class DeductionsController : Controller
    {
        private dbAptResourceEntities db = new dbAptResourceEntities();

        // GET: Deductions
        public async Task<ActionResult> Index()
        {
            return View(await db.Deductions.ToListAsync());
        }

        // GET: Deductions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deduction deduction = await db.Deductions.FindAsync(id);
            if (deduction == null)
            {
                return HttpNotFound();
            }
            return View(deduction);
        }

        // GET: Deductions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deductions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,EmpName,Designation,DeductionType,EffectiveDate,DeductionAmount,Reason,DeductionFrequency,ApprovalStatus,ApprovalDate")] Deduction deduction)
        {
            if (ModelState.IsValid)
            {
                // Check if a deduction with the same ID already exists
                var existingDeduction = await db.Deductions.FindAsync(deduction.ID);
                if (existingDeduction != null)
                {
                    ModelState.AddModelError("ID", "This ID already exists.");
                    return View(deduction); // Return view with error message
                }

                // If no duplicate, add and save the new deduction
                db.Deductions.Add(deduction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(deduction);
        }

        // GET: Deductions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deduction deduction = await db.Deductions.FindAsync(id);
            if (deduction == null)
            {
                return HttpNotFound();
            }
            return View(deduction);
        }

        // POST: Deductions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,EmpName,Designation,DeductionType,EffectiveDate,DeductionAmount,Reason,DeductionFrequency,ApprovalStatus,ApprovalDate")] Deduction deduction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deduction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(deduction);
        }

        // GET: Deductions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deduction deduction = await db.Deductions.FindAsync(id);
            if (deduction == null)
            {
                return HttpNotFound();
            }
            return View(deduction);
        }

        // POST: Deductions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Deduction deduction = await db.Deductions.FindAsync(id);
            db.Deductions.Remove(deduction);
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
