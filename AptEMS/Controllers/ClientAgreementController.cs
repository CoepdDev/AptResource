using AptEMS.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

namespace AptEMS.Controllers
{
    public class ClientAgreementController : Controller
    {
        private readonly AptEmsContext db = new AptEmsContext();

        public ActionResult Index()
        {
            var clientAgreements = db.ClientAgreements
                                     .Where(c => !c.IsDeleted)
                                     .ToList();
            return View(clientAgreements);
        }


        public ActionResult MarkAsPaid(int agreementId)
        {
            // Retrieve the payment record for the agreement
            var payment = db.Payments.FirstOrDefault(p => p.AgreementID == agreementId);

            // If the payment record exists, update the IsPaid field
            if (payment != null)
            {
                // If payment IsPaid is 0, mark it as 1 (Paid)
                if (payment.IsPaid == false)
                {
                    payment.IsPaid = true;
                    db.Entry(payment).State = EntityState.Modified;
                }
            }

            // Retrieve the client agreement and update the IsPaid field
            var clientAgreement = db.ClientAgreements.FirstOrDefault(ca => ca.AgreementID == agreementId);
            if (clientAgreement != null)
            {
                // If client agreement IsPaid is 0, mark it as 1 (Paid)
                if (clientAgreement.IsPaid == false)
                {
                    clientAgreement.IsPaid = true;
                    db.Entry(clientAgreement).State = EntityState.Modified;
                }
            }

            // Save changes to the database
            db.SaveChanges();

            // Redirect to the Index action to refresh the list
            return RedirectToAction("Index");
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
                // Save the uploaded files and handle any validation in the SaveUploadedFile method
                clientAgreement.CompanyLogo = SaveUploadedFile(CompanyLogo);
                clientAgreement.WatermarkLogo = SaveUploadedFile(WatermarkLogo);
                clientAgreement.LetterHeader = SaveUploadedFile(LetterHeader);
                clientAgreement.Footer = SaveUploadedFile(Footer);
                clientAgreement.DigitalSignature = SaveUploadedFile(DigitalSignature);

                // Add the new ClientAgreement to the database
                db.ClientAgreements.Add(clientAgreement);
                db.SaveChanges();

                // Redirect to the Index page after successful creation
                return RedirectToAction("Index");
            }

            // Return the view with the model if the state is invalid
            return View(clientAgreement);
        }

        private string SaveUploadedFile(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0)
                return null;

            // Allowed file extensions for photo uploads
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName)?.ToLower();

            // Check if the file extension is allowed
            if (!allowedExtensions.Contains(fileExtension))
                throw new InvalidOperationException("Only photo files (.jpg, .jpeg, .png, .gif) are allowed.");

            // Validate if the file is an actual image (photo)
            using (var img = Image.FromStream(file.InputStream))
            {
                if (img == null || img.Width == 0 || img.Height == 0)
                    throw new InvalidOperationException("The uploaded file is not a valid image.");
            }

            // Generate a unique file name
            string fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
            string filePath = Server.MapPath("~/Uploads/Resumes/" + fileName);

            // Save the file to the specified location
            file.SaveAs(filePath);

            // Return the relative file path to store in the database
            return "~/Uploads/Resumes/" + fileName;
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
                // Fetch existing record to preserve unchanged data
                var existingAgreement = db.ClientAgreements.AsNoTracking().FirstOrDefault(c => c.AgreementID == clientAgreement.AgreementID);

                if (existingAgreement == null)
                {
                    ModelState.AddModelError("", "Client Agreement not found.");
                    return View(clientAgreement);
                }

                // Handle CompanyLogo
                clientAgreement.CompanyLogo = CompanyLogo != null && CompanyLogo.ContentLength > 0
                    ? SaveUploadedFile(CompanyLogo)
                    : existingAgreement.CompanyLogo;

                // Handle WatermarkLogo
                clientAgreement.WatermarkLogo = WatermarkLogo != null && WatermarkLogo.ContentLength > 0
                    ? SaveUploadedFile(WatermarkLogo)
                    : existingAgreement.WatermarkLogo;

                // Handle LetterHeader
                clientAgreement.LetterHeader = LetterHeader != null && LetterHeader.ContentLength > 0
                    ? SaveUploadedFile(LetterHeader)
                    : existingAgreement.LetterHeader;

                // Handle Footer
                clientAgreement.Footer = Footer != null && Footer.ContentLength > 0
                    ? SaveUploadedFile(Footer)
                    : existingAgreement.Footer;

                // Handle DigitalSignature
                clientAgreement.DigitalSignature = DigitalSignature != null && DigitalSignature.ContentLength > 0
                    ? SaveUploadedFile(DigitalSignature)
                    : existingAgreement.DigitalSignature;

                // Update the database
                db.Entry(clientAgreement).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Return view if model state is invalid
            return View(clientAgreement);
        }


        /*// Helper Method to Save Uploaded Files
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
*/

        // GET: ClientAgreement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var clientAgreement = db.ClientAgreements.Find(id);

            if (clientAgreement == null || clientAgreement.IsDeleted)
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

            if (clientAgreement != null)
            {
                clientAgreement.IsDeleted = true; // Mark as deleted
                db.Entry(clientAgreement).State = EntityState.Modified;
                db.SaveChanges();
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
