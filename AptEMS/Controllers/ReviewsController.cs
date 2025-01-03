using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.Models; 

namespace AptEMS.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AptEmsContext _context = new AptEmsContext();

        // ******** CLIENT REVIEWS CRUD ********
        public ActionResult ManageReviews()
        {
            var reviews = _context.ClientReviews.ToList();
            return View(reviews);
        }

        [HttpGet]
        public ActionResult AddReview()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddReview(ClientReview review)
        {
            if (ModelState.IsValid)
            {
                _context.ClientReviews.Add(review);
                _context.SaveChanges();
                return RedirectToAction("ManageReviews");
            }
            return View(review);
        }

        [HttpGet]
        public ActionResult EditReview(int id)
        {
            var review = _context.ClientReviews.Find(id);
            if (review == null) return HttpNotFound();
            return View(review);
        }

        [HttpPost]
        public ActionResult EditReview(ClientReview review)
        {
            if (ModelState.IsValid)
            {
                var existingReview = _context.ClientReviews.Find(review.Id);
                if (existingReview == null) return HttpNotFound(); // Return 404 if not found

                existingReview.ReviewText = review.ReviewText;
                existingReview.Author = review.Author;

                _context.Entry(existingReview).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("ManageReviews");
            }
            return View(review);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReview(int id)
        {
            var review = _context.ClientReviews.Find(id);
            if (review != null)
            {
                _context.ClientReviews.Remove(review);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageReviews");
        }
        
        // ******** PARTNERSHIPS CRUD ********
        public ActionResult ManagePartnerships()
        {
            var partnerships = _context.Partnerships.ToList();
            return View(partnerships);
        }

        [HttpGet]
        public ActionResult AddPartnership()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPartnership(Partnership partnership, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(ImageFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/partnershippics"), fileName);

                    // Save the uploaded image
                    ImageFile.SaveAs(path);

                    // Set the ImagePath for the partnership
                    partnership.ImagePath = "~/Content/partnershippics/" + fileName;
                }

                // Save the new partnership to the database
                _context.Partnerships.Add(partnership);
                _context.SaveChanges();
                return RedirectToAction("ManagePartnerships");
            }

            return View(partnership);
        }


        [HttpGet]
        public ActionResult EditPartnership(int id)
        {
            var partnership = _context.Partnerships.Find(id);
            if (partnership == null)
            {
                return HttpNotFound(); // If no matching partnership is found, return 404.
            }
            return View(partnership);
        }

        [HttpPost]
        public ActionResult EditPartnership(Partnership partnership, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                var existingPartnership = _context.Partnerships.Find(partnership.Id);
                if (existingPartnership != null)
                {
                    // Handle image upload if a new file is provided
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(ImageFile.FileName);
                        string path = Path.Combine(Server.MapPath("~/Content/partnershippics"), fileName);

                        // Save the new image to the server
                        ImageFile.SaveAs(path);

                        // Update the ImagePath in the database
                        existingPartnership.ImagePath = "~/Content/partnershippics/" + fileName;
                    }

                    // Update the other fields
                    existingPartnership.AltText = partnership.AltText;

                    // Save the changes to the database
                    _context.Entry(existingPartnership).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                return RedirectToAction("ManagePartnerships");
            }
            return View(partnership);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePartnership(int id)
        {
            var partnership = _context.Partnerships.Find(id);
            if (partnership != null)
            {
                // Remove the partnership from the database
                _context.Partnerships.Remove(partnership);
                _context.SaveChanges();
            }
            return RedirectToAction("ManagePartnerships");
        }
        
    }
}
