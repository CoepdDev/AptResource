using AptEMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace AptEMS.Controllers
{
    public class QualificationController : Controller
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        // GET: Qualifications List
        public ActionResult Index()
        {
            var qualifications = GetQualifications();
            return View(qualifications);
        }

        // GET: Create Qualification
        public ActionResult Create()
        {


            return View();
        }

        // POST: Create Qualification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                CreateQualification(qualification);
                TempData["SuccessMessage"] = "Qualification created successfully!";
                return RedirectToAction("Index");
            }
            return View(qualification);
        }

        // GET: Edit Qualification
        public ActionResult Edit(int id)
        {
            var qualification = GetQualificationById(id);
            return View(qualification);
        }

        // POST: Edit Qualification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                UpdateQualification(qualification);
                TempData["SuccessMessage"] = "Qualification updated successfully!";
                return RedirectToAction("Index");
            }
            return View(qualification);
        }

        // POST: Qualification/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var qualification = GetQualificationById(id);

            if (qualification == null)
            {
                TempData["ErrorMessage"] = "Qualification not found.";
                return RedirectToAction("Index");
            }         

            DeleteQualification(id);

            TempData["SuccessMessage"] = "Qualification deleted successfully!";

            return RedirectToAction("Index");
        }


        // Helper to get qualifications for a dropdown list
        public SelectList GetQualificationsSelectList()
        {
            var qualifications = GetQualifications();
            return new SelectList(qualifications, "Id", "Name");
        }

        // API-style endpoint to fetch qualifications as JSON
        [HttpGet]
        public JsonResult GetQualificationsAsJson()
        {
            var qualifications = GetQualifications();
            return Json(qualifications, JsonRequestBehavior.AllowGet);
        }

        // Private methods for database operations
        private List<Qualification> GetQualifications()
        {
            var qualifications = new List<Qualification>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetAllQualifications", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        qualifications.Add(new Qualification
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }
            return qualifications;
        }

        private Qualification GetQualificationById(int id)
        {
            Qualification qualification = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT Id, Name FROM Qualifications WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        qualification = new Qualification
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString()
                        };
                    }
                }
            }
            return qualification;
        }

        private void CreateQualification(Qualification qualification)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("InsertQualification", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Name", qualification.Name);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void UpdateQualification(Qualification qualification)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UpdateQualification", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", qualification.Id);
                command.Parameters.AddWithValue("@Name", qualification.Name);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void DeleteQualification(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DeleteQualification", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
