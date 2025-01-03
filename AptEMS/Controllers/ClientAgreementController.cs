using AptEMS.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AptEMS.Controllers
{
    public class ClientAgreementController : Controller
    {
        private readonly AptEmsContext db = new AptEmsContext();

        // GET: ClientAgreement
        public ActionResult Index()
        {
            var agreements = db.ClientAgreements.ToList();
            return View(agreements);
        }

        public ActionResult DetailsView(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var clientAgreement = db.ClientAgreements
                .FirstOrDefault(ca => ca.AgreementID == id);

            if (clientAgreement == null)
            {
                return HttpNotFound();
            }

            return View(clientAgreement); 
        }



        // GET: ClientAgreement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientAgreement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientAgreements clientAgreement, HttpPostedFileBase CompanyLogo, HttpPostedFileBase WatermarkLogo, HttpPostedFileBase LetterHeader, HttpPostedFileBase Footer, HttpPostedFileBase DigitalSignature)
        {
            if (ModelState.IsValid)
            {
                clientAgreement.CompanyLogo = SaveUploadedFile(CompanyLogo);
                clientAgreement.WatermarkLogo = SaveUploadedFile(WatermarkLogo);
                clientAgreement.LetterHeader = SaveUploadedFile(LetterHeader);
                clientAgreement.Footer = SaveUploadedFile(Footer);
                clientAgreement.DigitalSignature = SaveUploadedFile(DigitalSignature);

                db.ClientAgreements.Add(clientAgreement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientAgreement);
        }

        // GET: ClientAgreement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clientAgreement = db.ClientAgreements.Find(id);
            if (clientAgreement == null)
            {
                return HttpNotFound();
            }

            // Pass the existing paths to the view to display the current files
            ViewBag.ExistingCompanyLogo = clientAgreement.CompanyLogo;
            ViewBag.ExistingWatermarkLogo = clientAgreement.WatermarkLogo;
            ViewBag.ExistingLetterHeader = clientAgreement.LetterHeader;
            ViewBag.ExistingFooter = clientAgreement.Footer;
            ViewBag.ExistingDigitalSignature = clientAgreement.DigitalSignature;

            return View(clientAgreement);
        }

        // POST: ClientAgreement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientAgreements clientAgreement, HttpPostedFileBase CompanyLogo, HttpPostedFileBase WatermarkLogo, HttpPostedFileBase LetterHeader, HttpPostedFileBase Footer, HttpPostedFileBase DigitalSignature)
        {
            if (ModelState.IsValid)
            {
                // Update file fields only when new files are uploaded
                if (CompanyLogo != null && CompanyLogo.ContentLength > 0)
                {
                    clientAgreement.CompanyLogo = SaveUploadedFile(CompanyLogo);
                }
                else
                {
                    clientAgreement.CompanyLogo = db.ClientAgreements.AsNoTracking().FirstOrDefault(c => c.AgreementID == clientAgreement.AgreementID)?.CompanyLogo;
                }

                if (WatermarkLogo != null && WatermarkLogo.ContentLength > 0)
                {
                    clientAgreement.WatermarkLogo = SaveUploadedFile(WatermarkLogo);
                }
                else
                {
                    clientAgreement.WatermarkLogo = db.ClientAgreements.AsNoTracking().FirstOrDefault(c => c.AgreementID == clientAgreement.AgreementID)?.WatermarkLogo;
                }

                if (LetterHeader != null && LetterHeader.ContentLength > 0)
                {
                    clientAgreement.LetterHeader = SaveUploadedFile(LetterHeader);
                }
                else
                {
                    clientAgreement.LetterHeader = db.ClientAgreements.AsNoTracking().FirstOrDefault(c => c.AgreementID == clientAgreement.AgreementID)?.LetterHeader;
                }

                if (Footer != null && Footer.ContentLength > 0)
                {
                    clientAgreement.Footer = SaveUploadedFile(Footer);
                }
                else
                {
                    clientAgreement.Footer = db.ClientAgreements.AsNoTracking().FirstOrDefault(c => c.AgreementID == clientAgreement.AgreementID)?.Footer;
                }

                if (DigitalSignature != null && DigitalSignature.ContentLength > 0)
                {
                    clientAgreement.DigitalSignature = SaveUploadedFile(DigitalSignature);
                }
                else
                {
                    clientAgreement.DigitalSignature = db.ClientAgreements.AsNoTracking().FirstOrDefault(c => c.AgreementID == clientAgreement.AgreementID)?.DigitalSignature;
                }

                // Save changes to the database
                db.Entry(clientAgreement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientAgreement);
        }

        // Helper Method to Save Uploaded Files
        private string SaveUploadedFile(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0)
                return null;

            // Allowed file extensions
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName)?.ToLower();

            // Check if the file extension is allowed
            if (!allowedExtensions.Contains(fileExtension))
                throw new InvalidOperationException("Only photo files (.jpg, .jpeg, .png, .gif) are allowed.");

            // Save the file
            string fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
            string filePath = Server.MapPath("~/Uploads/Resumes/" + fileName);
            file.SaveAs(filePath);
            return "~/Uploads/Resumes/" + fileName; // Return relative path to store in the database
        }


        // GET: ClientAgreement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clientAgreement = db.ClientAgreements.Find(id);
            if (clientAgreement == null)
            {
                return HttpNotFound();
            }
            return View(clientAgreement);
        }

        // POST: ClientAgreement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var clientAgreement = db.ClientAgreements.Find(id);
            db.ClientAgreements.Remove(clientAgreement);
            db.SaveChanges();
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
