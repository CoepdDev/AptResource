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
    public class SkillsController : Controller
    {
        private AptEmsContext db = new AptEmsContext();

        // GET: Skills
        public async Task<ActionResult> Index()
        {
            return View(await db.Skills.ToListAsync());
        }

        

        // GET: Skills/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SkillID,SkillName,IsActive")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Skills.Add(skill);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(skill);
        }

        // GET: Skills/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SkillID,SkillName,IsActive")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skill).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Skill skill = await db.Skills.FindAsync(id);
            if (skill != null)
            {
                db.Skills.Remove(skill);
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
