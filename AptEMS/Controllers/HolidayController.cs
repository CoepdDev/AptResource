using AptEMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AptEMS.Controllers
{
    public class HolidayController : Controller
    {
        private HolidayDbContext _context = new HolidayDbContext();

        public ActionResult Index()
        {
            var holidays = _context.GetHolidays();
            return View(holidays);
        }

        public ActionResult Manage(int? id)
        {
            if (id == null)
                return View(new Holiday());

            var holiday = _context.Holidays.Find(id);
            if (holiday == null)
                return HttpNotFound();
           
            return View(holiday);
        }

        [HttpPost]
        public ActionResult Manage(Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                if (holiday.HolidayId == 0)
                    _context.AddHoliday(holiday);
                else
                    _context.UpdateHoliday(holiday);
                TempData["SuccessMessage"] = "Holiday created successfully!";
                return RedirectToAction("Index");
            }
            
            return View(holiday);
        }

        public ActionResult Edit(int id)
        {
            var holiday = _context.Holidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }

            return View(holiday); // Pass the holiday data to the Edit view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                var existingHoliday = _context.Holidays.Find(holiday.HolidayId);
                if (existingHoliday != null)
                {
                    // Update the holiday details
                    existingHoliday.HolidayName = holiday.HolidayName;
                    existingHoliday.FromDate = holiday.FromDate;
                    existingHoliday.ToDate = holiday.ToDate;

                    // Recalculate Days
                    existingHoliday.Days = (holiday.ToDate - holiday.FromDate).Days + 1;

                    _context.SaveChanges(); // Save the changes
                }
                TempData["SuccessMessage"] = "Holiday Edited successfully!";
                return RedirectToAction("Index");
            }
            
            return View(holiday); // Return the form with validation errors if any
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int HolidayId)
        {
            var holiday = _context.Holidays.Find(HolidayId);
            if (holiday != null)
            {
                _context.Holidays.Remove(holiday); // Remove the holiday record
                _context.SaveChanges();           // Commit changes to the database
            }
            TempData["SuccessMessage"] = "Holiday Deleted successfully!";
            return RedirectToAction("Index");
        }

    }

}