using AptEMS.Models;
using AptEMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AptEMS.Controllers
{
    public class JobController : Controller
    {
        // Connection string for direct SQL Server queries
        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        // Database context for Entity Framework operations
        private readonly AptEmsContext dbContext = new AptEmsContext();

        private readonly AptEmsContext _context = new AptEmsContext();


        public ActionResult FindAJob()
        {
            List<JobPost> activeJobs = new List<JobPost>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Update query to include expiry date condition
                string query = "SELECT * FROM JobPosts WHERE IsActive = 1 AND ExpiryDate >= @Today";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Today", DateTime.Now.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            activeJobs.Add(new JobPost
                            {
                                JobPostID = Convert.ToInt32(reader["JobPostID"]),
                                EmploymentType = reader["EmploymentType"].ToString(),
                                JobType = reader["JobType"].ToString(),
                                JobTitle = reader["JobTitle"].ToString(),
                                MinExperience = Convert.ToInt32(reader["MinExperience"]),
                                JobDescription = reader["JobDescription"].ToString(),
                                ExpiryDate = Convert.ToDateTime(reader["ExpiryDate"])
                            });
                        }
                    }
                }
            }

            return View(activeJobs); // Pass active jobs to the FindAJob view
        }


        // Display the job application form
        public ActionResult Application(int jobId)
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



            if (dbContext.Strengths.Any())
            {
                ViewBag.Strength = new SelectList(dbContext.Strengths, "StrengthName", "StrengthName");
            }
            else
            {
                ViewBag.Strength = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            if (dbContext.Locations.Any())
            {
                ViewBag.Location = new SelectList(dbContext.Locations, "LocationName", "LocationName");
            }
            else
            {
                ViewBag.Location = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            dbAptResourceEntities2 db = new dbAptResourceEntities2();

            if (db.Designations.Any())
            {
                ViewBag.Designation = new SelectList(db.Designations, "Designation1", "Designation1");
            }
            else
            {
                ViewBag.Designation = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            var JobPost = dbContext.JobPosts.Find(jobId);
            if (JobPost == null)
            {
                return HttpNotFound();
            }

            var viewModel = new JobApplicationViewModel
            {
                JobPost = JobPost,
                JobApplication = new JobApplication() // Initialize a new JobApplication for form input
            };

            return View(viewModel);
        }

        // Handle form submission 
        [HttpPost]
        public ActionResult Application(JobApplicationViewModel viewModel, HttpPostedFileBase resume)
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
                ViewBag.Skill = new SelectList(dbContext.Skills, "Skill" +
                    "Name", "SkillName");
            }
            else
            {
                ViewBag.Skill = new SelectList(Enumerable.Empty<SelectListItem>());
            }



            if (dbContext.Strengths.Any())
            {
                ViewBag.Strength = new SelectList(dbContext.Strengths, "StrengthName", "StrengthName");
            }
            else
            {
                ViewBag.Strength = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            if (dbContext.Locations.Any())
            {
                ViewBag.Location = new SelectList(dbContext.Locations, "LocationName", "LocationName");
            }
            else
            {
                ViewBag.Location = new SelectList(Enumerable.Empty<SelectListItem>());
            }


            dbAptResourceEntities2 db = new dbAptResourceEntities2();

           
            if (db.Designations.Any())
            {
                ViewBag.Designation = new SelectList(db.Designations, "Designation1", "Designation1");
            }
            else
            {
                ViewBag.Designation = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            if (ModelState.IsValid)
            {
                // Check for file upload
                if (resume != null && resume.ContentLength > 0)
                {
                    // File extension validation
                    string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".rtf" };
                    string fileExtension = Path.GetExtension(resume.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("resume", "Invalid file type. Only PDF, DOC, DOCX, RTF files are allowed.");
                        return View(viewModel);
                    }

                    // File size validation (5MB)
                    if (resume.ContentLength > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("resume", "The file size cannot exceed 5MB.");
                        return View(viewModel);
                    }

                    // Save file to the server
                    string fileName = Path.GetFileName(resume.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Uploads/Resumes"), fileName);
                    resume.SaveAs(filePath);

                    // Save the file path in the model
                    viewModel.JobApplication.ResumeFilePath = "~/Uploads/Resumes/" + fileName;
                }

                // Save the JobApplication to the database
                dbContext.JobApplications.Add(viewModel.JobApplication);
                dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Application submitted successfully!";
                return RedirectToAction("FindAJob", "Job");
            }

            TempData["ErrorMessage"] = "There was an error submitting your application. Please check your inputs.";
            return View(viewModel);
        }

        // Action to delete a job application
        [HttpPost]
        public ActionResult DeleteApplication(int id)
        {
            var application = dbContext.JobApplications.Find(id);
            if (application != null)
            {
                dbContext.JobApplications.Remove(application);
                dbContext.SaveChanges();
            }

            return RedirectToAction("ApplicationList");
        }

        // Action to list all applications with details
        public ActionResult ApplicationList()
        {
            // Fetch data from JobApplicationForm
            var jobApplicationFormData = dbContext.JobApplicationForms.Select(x => new CombinedJobApplicationViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Mobile = x.Mobile,
                FatherName = x.FatherName,
                Dob = x.Dob,
                Specialization = x.Specialization,
                TotalExperience = x.TotalExperience,
                RelevantExperience = x.RelevantExperience,
                KeySkills = x.KeySkills,
                Strengths = x.Strengths,
                Designation = x.Designation,
                CurrentLocation = x.CurrentLocation,
                PresentCTC = x.PresentCTC,
                ExpectedCTC = x.ExpectedCTC,
                NoticePeriod = x.NoticePeriod,
                Education = x.Education, 
                Gender = x.Gender,
                CreatedBy = x.CreatedBy,
                ResumeFilePath = x.ResumeFilePath
            }).ToList();

            // Fetch data from JobApplication
            var jobApplicationData = dbContext.JobApplications.Select(x => new CombinedJobApplicationViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Mobile = x.Mobile,
                FatherName = x.FatherName,
                Dob = x.Dob,
                Specialization = x.Specialization,
                TotalExperience = x.TotalExperience,
                RelevantExperience = x.RelevantExperience,
                KeySkills = x.KeySkills,
                Strengths = x.Strengths,
                Designation = x.Designation,
                CurrentLocation = x.CurrentLocation,
                PresentCTC = x.PresentCTC,
                ExpectedCTC = x.ExpectedCTC,
                NoticePeriod = x.NoticePeriod,
                Education = x.Education,
                Gender = x.Gender,
                CreatedBy = x.CreatedBy,
                ResumeFilePath = x.ResumeFilePath
            }).ToList();

            // Combine both data sets
            var combinedData = jobApplicationFormData.Concat(jobApplicationData).ToList();

            return View(combinedData);
        }




        public ActionResult BuyNow()
        {
            var activePackages = dbContext.PricingPackages.Where(p => p.IsActive).ToList();

            // Pass the success message from TempData to the view
            ViewBag.SuccessMessage = TempData["SuccessMessage"];

            return View(activePackages);
        }


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



            if (dbContext.Strengths.Any())
            {
                ViewBag.Strength = new SelectList(dbContext.Strengths, "StrengthName", "StrengthName");
            }
            else
            {
                ViewBag.Strength = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            if (dbContext.Locations.Any())
            {
                ViewBag.Location = new SelectList(dbContext.Locations, "LocationName", "LocationName");
            }
            else
            {
                ViewBag.Location = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            dbAptResourceEntities2 db = new dbAptResourceEntities2();

            if (db.Designations.Any())
            {
                ViewBag.Designation = new SelectList(db.Designations, "Designation1", "Designation1");
            }
            else
            {
                ViewBag.Designation = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            return View();
        }

        // POST: Create Job Application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobApplicationForm form, HttpPostedFileBase resume)
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



            if (dbContext.Strengths.Any())
            {
                ViewBag.Strength = new SelectList(dbContext.Strengths, "StrengthName", "StrengthName");
            }
            else
            {
                ViewBag.Strength = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            if (dbContext.Locations.Any())
            {
                ViewBag.Location = new SelectList(dbContext.Locations, "LocationName", "LocationName");
            }
            else
            {
                ViewBag.Location = new SelectList(Enumerable.Empty<SelectListItem>());
            }


            dbAptResourceEntities2 db = new dbAptResourceEntities2();


            if (db.Designations.Any())
            {
                ViewBag.Designation = new SelectList(db.Designations, "Designation1", "Designation1");
            }
            else
            {
                ViewBag.Designation = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            // Check if a file is uploaded
            if (resume != null && resume.ContentLength > 0)
            {
                // Validate file extension (PDF, DOC, DOCX, RTF)
                string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".rtf" };
                string fileExtension = Path.GetExtension(resume.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ResumeFilePath", "Invalid file type. Only PDF, DOC, DOCX, RTF files are allowed.");

                    return View(form);
                }

                // Validate file size (limit to 5MB)
                if (resume.ContentLength > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("ResumeFilePath", "The file size cannot exceed 5MB.");
                    return View(form);
                }

                // Save the file to the server
                string fileName = Path.GetFileName(resume.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Uploads/Resumes"), fileName);

                // Check if file already exists, if yes, rename it
                int counter = 1;
                while (System.IO.File.Exists(filePath))
                {
                    filePath = Path.Combine(Server.MapPath("~/Uploads/Resumes"), Path.GetFileNameWithoutExtension(fileName) + "_" + counter + Path.GetExtension(fileName));
                    counter++;
                }

                resume.SaveAs(filePath);

                // Save the relative file path in the model
                form.ResumeFilePath = "~/Uploads/Resumes/" + Path.GetFileName(filePath);
            }

            // Add the job application form to the database
            _context.JobApplicationForms.Add(form);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Job application submitted successfully!";
            return RedirectToAction("Create"); // Redirect back to the form

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Services()
        {
            return View();
        }
    }
}
