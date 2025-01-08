using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.DAL;
using AptEMS.Models;

namespace AptEMS.Controllers
{
    public class SelectedCandidatesController : Controller
    {
        DAL.SelectedCandidates objdalemp = new DAL.SelectedCandidates();
        //DAL.SecondRound objdalemp = new DAL.SecondRound();
        public ActionResult Index()
        {

            List<Models.SelectedCandidates> ie = objdalemp.GetTSelected();
            return View(ie);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.SelectedCandidates e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.AddSelected(e1);
                if (i == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (i == -1)
                {
                    // Handle duplicate ID case
                    //ViewBag.ErrorMessage = "This ID already exists.";

                    ModelState.AddModelError("Email", "This Email already exists.");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            Models.SelectedCandidates e1 = new Models.SelectedCandidates();
            e1.Mobile = id;
            int i = objdalemp.DeleteSelected(e1);
            if (i == 1)
            {
                return RedirectToAction("index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            Models.SelectedCandidates e1 = new Models.SelectedCandidates();
            e1.Mobile = id;
            e1 = objdalemp.SearchSelected(e1);

            return View(e1);
        }
        [HttpPost]
        public ActionResult Edit(Models.SelectedCandidates e1)
        {
            if (ModelState.IsValid)
            {
                int i = objdalemp.UpdateSelected(e1);
                if (i == 1)
                {
                    return RedirectToAction("index");
                }
            }

            return View(e1);
        }

        //[HttpPost]
        //public JsonResult InsertOfferLetterReleased(AptEMS.Models.SelectedCandidates model)
        //{
        //    try
        //    {
        //        using (var context = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString))
        //        {
        //            context.Open();
        //            using (var cmd = new SqlCommand("sp_InsertOfferLetterReleased", context))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@Name", model.Name);
        //                cmd.Parameters.AddWithValue("@Role", model.Role);
        //                cmd.Parameters.AddWithValue("@InterviewerName", model.InterviewerName);
        //                cmd.Parameters.AddWithValue("@Date", model.Date);
        //                cmd.Parameters.AddWithValue("@Remark", model.Remark);
        //                cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
        //                cmd.Parameters.AddWithValue("@Email", model.Email);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        return Json(new { success = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}

        [HttpPost]
        public JsonResult InsertOfferLetterReleased(AptEMS.Models.SelectedCandidates model)
        {
            try
            {
                using (var context = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString))
                {
                    context.Open();

                    // Begin a transaction to ensure both operations succeed together
                    using (var transaction = context.BeginTransaction())
                    {
                        try
                        {
                            // Insert into OfferLetterReleased
                            using (var cmdInsert = new SqlCommand("sp_InsertOfferLetterReleased", context, transaction))
                            {
                                cmdInsert.CommandType = CommandType.StoredProcedure;
                                cmdInsert.Parameters.AddWithValue("@Name", model.Name);
                                cmdInsert.Parameters.AddWithValue("@Role", model.Role);
                                cmdInsert.Parameters.AddWithValue("@InterviewerName", model.InterviewerName);
                                cmdInsert.Parameters.AddWithValue("@Date", model.Date);
                                cmdInsert.Parameters.AddWithValue("@Remark", model.Remark);
                                cmdInsert.Parameters.AddWithValue("@Mobile", model.Mobile);
                                cmdInsert.Parameters.AddWithValue("@Email", model.Email);
                                cmdInsert.ExecuteNonQuery();
                            }

                            // Delete from SelectedCandidates
                            using (var cmdDelete = new SqlCommand("DELETE FROM SelectedCandidates WHERE Mobile = @Mobile", context, transaction))
                            {
                                cmdDelete.Parameters.AddWithValue("@Mobile", model.Mobile);
                                cmdDelete.ExecuteNonQuery();
                            }

                            // Commit the transaction
                            transaction.Commit();
                        }
                        catch
                        {
                            // Rollback the transaction on error
                            transaction.Rollback();
                            throw;
                        }
                    }
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



    }
}