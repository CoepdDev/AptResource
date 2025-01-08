using AptEMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AptEMS.Controllers
{
    public class JobPostController : Controller
    {
        private readonly AptEmsContext _context;

        public JobPostController()
        {
            _context = new AptEmsContext();
        }

        // Dispose the context to release resources
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index(string filter = "all")
        {
            // Fetch all job posts initially
            var jobPosts = _context.JobPosts.AsQueryable();

            // Apply filtering logic based on the `filter` parameter
            if (filter == "active")
            {
                jobPosts = jobPosts.Where(j => j.IsActive);
            }
            else if (filter == "inactive")
            {
                jobPosts = jobPosts.Where(j => !j.IsActive);
            }

            // Convert the filtered results to a list and pass to the view
            return View(jobPosts.ToList());
        }

        // GET: JobPost/Details/5
        public ActionResult Details(int id)
        {
            var jobPost = _context.JobPosts.SingleOrDefault(j => j.JobPostID == id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }
            return View(jobPost);
        }

        // GET: JobPost/Create
        public ActionResult Create()
        {
            AptEmsContext dbContext = new AptEmsContext();

            if (dbContext.Qualifications.Any())
            {
                ViewBag.Qualification = new SelectList(dbContext.Qualifications, "Name", "Name");
            }
            else
            {
                ViewBag.Qualification = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            if (dbContext.Skills.Any())
            {
                ViewBag.Skill = new SelectList(dbContext.Skills, "SkillName", "SkillName");
            }
            else
            {
                ViewBag.Skill = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            if (dbContext.Locations.Any())
            {
                ViewBag.Location = new SelectList(dbContext.Locations, "LocationName", "LocationName");
            }
            else
            {
                ViewBag.Location = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            return View();
        }

        // POST: JobPost/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(JobPost jobPost)
        {
            AptEmsContext dbContext = new AptEmsContext();

            if (dbContext.Qualifications.Any())
            {
                ViewBag.Qualification = new SelectList(dbContext.Qualifications, "Name", "Name");
            }
            else
            {
                ViewBag.Qualification = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            if (dbContext.Skills.Any())
            {
                ViewBag.Skill = new SelectList(dbContext.Skills, "SkillName", "SkillName");
            }
            else
            {
                ViewBag.Skill = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            if (dbContext.Locations.Any())
            {
                ViewBag.Location = new SelectList(dbContext.Locations, "LocationName", "LocationName");
            }
            else
            {
                ViewBag.Location = new SelectList(Enumerable.Empty<SelectListItem>());
            }


            if (ModelState.IsValid)
            {
                _context.JobPosts.Add(jobPost);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Job application submitted successfully!";
                return RedirectToAction("Index");
            }
            
            return View(jobPost);
        }

        // GET: JobPost/Edit/5
        public ActionResult Edit(int id)
        {
            AptEmsContext dbContext = new AptEmsContext();

            if (dbContext.Qualifications.Any())
            {
                ViewBag.Qualification = new SelectList(dbContext.Qualifications, "Name", "Name");
            }
            else
            {
                ViewBag.Qualification = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            if (dbContext.Skills.Any())
            {
                ViewBag.Skill = new SelectList(dbContext.Skills, "SkillName", "SkillName");
            }
            else
            {
                ViewBag.Skill = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            if (dbContext.Locations.Any())
            {
                ViewBag.Location = new SelectList(dbContext.Locations, "LocationName", "LocationName");
            }
            else
            {
                ViewBag.Location = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            var jobPost = _context.JobPosts.SingleOrDefault(j => j.JobPostID == id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }

            return View(jobPost);
        }

        // POST: JobPost/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(JobPost jobPost)
        {
            AptEmsContext dbContext = new AptEmsContext();

            if (dbContext.Qualifications.Any())
            {
                ViewBag.Qualification = new SelectList(dbContext.Qualifications, "Name", "Name");
            }
            else
            {
                ViewBag.Qualification = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            if (dbContext.Skills.Any())
            {
                ViewBag.Skill = new SelectList(dbContext.Skills, "SkillName", "SkillName");
            }
            else
            {
                ViewBag.Skill = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            if (dbContext.Locations.Any())
            {
                ViewBag.Location = new SelectList(dbContext.Locations, "LocationName", "LocationName");
            }
            else
            {
                ViewBag.Location = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            if (ModelState.IsValid)
            {
                var jobPostInDb = _context.JobPosts.Single(j => j.JobPostID == jobPost.JobPostID);
                jobPostInDb.JobType = jobPost.JobType;
                jobPostInDb.EmploymentType = jobPost.EmploymentType;
                jobPostInDb.ScheduledDate = jobPost.ScheduledDate;
                jobPostInDb.ExpiryDate = jobPost.ExpiryDate;
                jobPostInDb.JobTitle = jobPost.JobTitle;
                jobPostInDb.MinExperience = jobPost.MinExperience;
                jobPostInDb.MaxExperience = jobPost.MaxExperience;
                jobPostInDb.Skills = jobPost.Skills;
                jobPostInDb.JobDescription = jobPost.JobDescription;
                jobPostInDb.JobLocation = jobPost.JobLocation;
                jobPostInDb.MinSalary = jobPost.MinSalary;
                jobPostInDb.MaxSalary = jobPost.MaxSalary;
                jobPostInDb.Industry = jobPost.Industry;
                jobPostInDb.FunctionsAndRoles = jobPost.FunctionsAndRoles;
                jobPostInDb.Education = jobPost.Education;
                jobPostInDb.IsActive = jobPost.IsActive;
                jobPostInDb.RecruiterName = jobPost.RecruiterName;
                jobPostInDb.RecruiterDesignation = jobPost.RecruiterDesignation;
                jobPostInDb.RecruiterPhoneNumber = jobPost.RecruiterPhoneNumber;
                jobPostInDb.RecruiterEmail = jobPost.RecruiterEmail;
                jobPostInDb.RecruiterCompanyName = jobPost.RecruiterCompanyName;
                jobPostInDb.RecruiterAboutCompany = jobPost.RecruiterAboutCompany;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var jobPost = _context.JobPosts.SingleOrDefault(j => j.JobPostID == id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }

            _context.JobPosts.Remove(jobPost);
            _context.SaveChanges();

            // Set a success message
            TempData["SuccessMessage"] = "The job posting was successfully deleted.";
            return RedirectToAction("Index");
        }

    }
}