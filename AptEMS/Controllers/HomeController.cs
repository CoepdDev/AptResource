using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AptEMS.Models;
using Dapper;

namespace AptEMS.Controllers
{
    public class HomeController : Controller
    {
        
        

   
        private readonly AptEmsContext _context = new AptEmsContext();

        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                ClientReviews = _context.ClientReviews.ToList(),
                Partnerships = _context.Partnerships.ToList(),
                Statistics = _context.Statistics.Where(s => s.IsActive).ToList(),
                //UploadedImages = _context.UploadedImages.ToList(),
                 UploadedImages = _context.UploadedImages.Where(img => img.IsActive).ToList()

            };
            return View(viewModel);
        }


        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Refundpolicy()
        {
            return View();
        }

        public ActionResult TermsConditions()
        {
            return View();
        }
       


        public ActionResult About()
        {
            
            return View();
        }

        private readonly string _connectionString;

        public HomeController()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

        public ActionResult Contact()
        {
            return View(new ContactFormModel()); // Return a new instance of the model to reset the form
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Save data to the database
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        string sql = "INSERT INTO ContactForm (FullName, CompanyName, EmailAddress, PhoneNumber, Designation, HiringInterest, Requirement) " +
                                     "VALUES (@FullName, @CompanyName, @EmailAddress, @PhoneNumber, @Designation, @HiringInterest, @Requirement)";
                        await connection.ExecuteAsync(sql, model);
                    }

                    // Set the success message to TempData
                    TempData["Message"] = "Thank you for contacting us. We will get back to you soon!";
                    return RedirectToAction("Contact");
                }
                catch (Exception)
                {
                    TempData["Message"] = "Sorry, something went wrong. Please try again.";
                }
            }
            else
            {
                TempData["Message"] = "Please fill in all required fields correctly.";
            }

            return View(model); // Return the same view with validation messages
        }

        public ActionResult AdminContactForm()
        {
            IEnumerable<ContactFormModel> contactForms = GetContactForms();
            return View(contactForms);
        }

        private IEnumerable<ContactFormModel> GetContactForms()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT FullName, CompanyName, EmailAddress, PhoneNumber, Designation, HiringInterest, Requirement, CreatedAt FROM ContactForm";
                return connection.Query<ContactFormModel>(sql);
            }
        }

        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
