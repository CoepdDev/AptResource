using AptEMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace AptEMS.Controllers
{
    public class ClientController : Controller
    {
        DAL.Employee objdalemp = new DAL.Employee();

        private readonly AptEmsContext dbContext = new AptEmsContext();

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




        public ActionResult Enroll(int packageId)
        {
            var package = dbContext.PricingPackages.FirstOrDefault(p => p.PackageId == packageId && p.IsActive);
            if (package == null)
            {
                return HttpNotFound("Package not found or inactive.");
            }

            ViewBag.PackageName = package.PackageName;
            ViewBag.PackagePrice = package.Price;
            ViewBag.BillingCycle = package.BillingCycle;

            var model = new Enrollment { PackageId = packageId };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enroll(Enrollment model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new AptEmsContext())
                {
                    context.Enrollments.Add(model);
                    context.SaveChanges();
                }

                // Set the success message and redirect to the BuyNow page
                TempData["SuccessMessage"] = "Enrollment successful!";

                // Redirect to BuyNow page in the current controller
                return RedirectToAction("BuyNow");
            }

            // If validation fails, reload package details
            var package = dbContext.PricingPackages.FirstOrDefault(p => p.PackageId == model.PackageId);
            ViewBag.PackageName = package?.PackageName;
            ViewBag.PackagePrice = package?.Price;
            ViewBag.BillingCycle = package?.BillingCycle;

            return View(model); // Return the form with validation errors
        }




    }
}
