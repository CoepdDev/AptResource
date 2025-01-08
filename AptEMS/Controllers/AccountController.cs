using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class AccountController : Controller
    {
        private AptEmsContext db = new AptEmsContext();

        // GET: Signup
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Execute the stored procedure
                    db.Database.ExecuteSqlCommand(
                        "EXEC sp_CreateUserAccount @FullName, @Email, @Password, @PhoneNumber",
                        new SqlParameter("FullName", model.FullName),
                        new SqlParameter("Email", model.Email),
                        new SqlParameter("Password", model.Password),
                        new SqlParameter("PhoneNumber", model.PhoneNumber)
                    );

                    return RedirectToAction("Login");
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("The email is already registered."))
                    {
                        // Display a user-friendly message for duplicate emails
                        ModelState.AddModelError("Email", "The email is already registered. Please use a different email.");
                    }
                    else
                    {
                        // Handle other SQL exceptions
                        ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                    }
                }
            }

            return View(model);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserAccounts
                    .SqlQuery("EXEC sp_AuthenticateUser @Email, @Password",
                        new SqlParameter("Email", model.Email),
                        new SqlParameter("Password", model.Password))
                    .FirstOrDefault();

                if (user != null)
                {
                    // Successful login logic here
                    return RedirectToAction("Dashboard", "Home");
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(model);
        }

        public ActionResult Userdetails()
        {
            var candidates = db.UserAccounts.ToList();
            return View(candidates);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserdetails(int id)
        {
            var candidate = db.UserAccounts.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }

            db.UserAccounts.Remove(candidate);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Candidate deleted successfully.";
            return RedirectToAction("Userdetails");
        }

        

    }
}
