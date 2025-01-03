using AptEMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AptEMS.Controllers
{
    public class ClientController : Controller
    {
        DAL.Employee objdalemp = new DAL.Employee();

        [HttpGet]
        public ActionResult EnrollmentList(string searchQuery, string selectedEmployeeName)
        {
            // Populate the employee dropdown from DAL
            var employeeList = objdalemp.Getemp()
                .Select(e => new SelectListItem
                {
                    Value = e.FirstName,
                    Text = e.FirstName
                }).ToList();

            ViewBag.EmployeeList = new SelectList(employeeList, "Value", "Text");

            using (var context = new AptEmsContext())
            {
                var enrollments = context.Enrollments.AsQueryable();

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    searchQuery = searchQuery.ToLower();
                    enrollments = enrollments.Where(e =>
                        e.FirstName.ToLower().Contains(searchQuery) ||
                        e.LastName.ToLower().Contains(searchQuery) ||
                        e.Email.ToLower().Contains(searchQuery) ||
                        e.Mobile.Contains(searchQuery) ||
                        e.Company.ToLower().Contains(searchQuery) ||
                        e.Location.ToLower().Contains(searchQuery) ||
                        e.Remarks.ToLower().Contains(searchQuery));
                }

                return View(enrollments.ToList());
            }
        }

        [HttpPost]
        public ActionResult AssignEmployee(int id, string selectedEmployeeName)
        {
            if (string.IsNullOrEmpty(selectedEmployeeName))
            {
                TempData["Error"] = "Please select an employee before assigning.";
                return RedirectToAction("EnrollmentList");
            }

            using (var context = new AptEmsContext())
            {
                var enrollment = context.Enrollments.Find(id);

                if (enrollment != null)
                {
                    // Save the assigned enrollment
                    var assignedEnrollment = new AssignedEnrollment
                    {
                        FirstName = enrollment.FirstName,
                        LastName = enrollment.LastName,
                        Email = enrollment.Email,
                        Mobile = enrollment.Mobile,
                        Company = enrollment.Company,
                        Location = enrollment.Location,
                        Remarks = enrollment.Remarks,
                        AssignedEmployee = selectedEmployeeName,
                        AssignedDate = DateTime.Now
                    };

                    context.AssignedEnrollments.Add(assignedEnrollment);

                    // Remove from unassigned enrollments
                    context.Enrollments.Remove(enrollment);
                    context.SaveChanges();

                    TempData["Success"] = $"Client assigned to {selectedEmployeeName}.";
                }
                else
                {
                    TempData["Error"] = "Client not found.";
                }
            }

            return RedirectToAction("EnrollmentList");
        }

        public ActionResult AssignedClients()
        {
            using (var context = new AptEmsContext())
            {
                var assignedClients = context.AssignedEnrollments.ToList();
                return View(assignedClients);
            }
        }




        // Action to display the Enroll form
        public ActionResult Enroll()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitEnrollment(Enrollment model)
        {
            if (ModelState.IsValid)
            {
                // Save the enrollment data to the database
                using (var context = new AptEmsContext())
                {
                    context.Enrollments.Add(model);
                    context.SaveChanges();
                }

                // Set a success message in TempData
                TempData["SuccessMessage"] = "Your enrollment has been submitted successfully!";

                // Return the same view to show the success message temporarily
                return View("Enroll");
            }

            // If validation fails, return the view with validation errors
            return View("Enroll", model);
        }

    }
}
