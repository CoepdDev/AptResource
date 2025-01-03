using System.Linq;
using System.Web.Mvc;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class PricingPackageController : Controller
    {
        private readonly AptEmsContext db = new AptEmsContext();

        // GET: PricingPackage
        public ActionResult Index()
        {
            var pricingPackages = db.PricingPackages.ToList();
            return View(pricingPackages);
        }

        // GET: PricingPackage/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PricingPackage package)
        {
            if (ModelState.IsValid)
            {
                db.PricingPackages.Add(package);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(package);
        }

        // GET: PricingPackage/Edit/5
        public ActionResult Edit(int id)
        {
            var package = db.PricingPackages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PricingPackage package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(package);
        }

        // POST: PricingPackage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var package = db.PricingPackages.Find(id);
            if (package != null)
            {
                db.PricingPackages.Remove(package);
                db.SaveChanges();
            }
            return RedirectToAction("Index"); // After deletion, redirect back to Index page
        }
    }
}
