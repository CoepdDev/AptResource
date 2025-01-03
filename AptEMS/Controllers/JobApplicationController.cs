using System.Linq;
using System.Web.Mvc;
using AptEMS.ViewModels;
using AptEMS.Models;
using System.Data.Entity;

public class JobApplicationController : Controller
{
    private readonly AptEmsContext _context = new AptEmsContext(); // Use your database context

    [HttpGet]
    public ActionResult MapApplicationToJob(int applicationId)
    {
        // Fetch data from JobApplicationForm
        var jobApplicationForm = _context.JobApplicationForms
            .Where(x => x.Id == applicationId)
            .Select(x => new CombinedJobApplicationViewModel
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
            })
            .FirstOrDefault();

        // Combine the data
        var application = jobApplicationForm;
        if (application == null)
        {
            return HttpNotFound("Application not found.");
        }

        // Fetch and filter job posts by matching skills
        var applicantSkills = application.KeySkills?.Split(',').Select(s => s.Trim()).ToList();
        var matchedJobPosts = _context.JobPosts
            .Where(job => job.Skills != null && applicantSkills.Any(skill => job.Skills.Contains(skill)))
            .ToList();

        // Pass filtered job posts to the view
        ViewBag.JobPosts = matchedJobPosts;
        return View(application); // Passing the combined application view model to the view
    }


    [HttpGet]
    public ActionResult ViewJobApplications(int jobPostId)
    {
        // Fetch the specific job post
        var jobPost = _context.JobPosts
            .Where(x => x.JobPostID == jobPostId)
            .FirstOrDefault();

        if (jobPost == null)
        {
            return HttpNotFound("Job post not found.");
        }

        // Extract Skills from the Job Post (Assuming it's a comma-separated string)
        var requiredSkills = jobPost.Skills.Split(',').Select(skill => skill.Trim()).ToList();

        // Fetch applications from both JobApplicationForms and JobApplications
        var jobApplicationFormsData = _context.JobApplicationForms
            .Where(x => x.KeySkills != null)
            .Select(x => new CombinedJobApplicationViewModel
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

        var jobApplicationsData = _context.JobApplications
            .Where(x => x.KeySkills != null)
            .Select(x => new CombinedJobApplicationViewModel
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

        // Combine both lists
        var combinedApplicants = jobApplicationFormsData.Concat(jobApplicationsData).ToList();

        // Filter applicants based on matching KeySkills with job post's required skills
        var matchedApplicants = combinedApplicants.Where(app =>
        {
            var applicantSkills = app.KeySkills.Split(',').Select(skill => skill.Trim()).ToList();
            return applicantSkills.Intersect(requiredSkills).Any(); // Check for any matching skills
        }).ToList();

        // Pass matched applicants to the View
        ViewBag.MatchedApplicants = matchedApplicants;
        ViewBag.JobPost = jobPost; // Passing the job post details for display in the view

        return View();
    }







}
