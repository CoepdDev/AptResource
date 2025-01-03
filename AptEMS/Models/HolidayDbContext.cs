using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class HolidayDbContext : DbContext
    {
        public HolidayDbContext() : base("cs")
        {
        }
        public DbSet<Holiday> Holidays { get; set; }

        public void AddHoliday(Holiday holiday)
        {
            Database.ExecuteSqlCommand("AddHoliday @p0, @p1, @p2",
                holiday.HolidayName, holiday.FromDate, holiday.ToDate);
        }

        public void UpdateHoliday(Holiday holiday)
        {
            Database.ExecuteSqlCommand("UpdateHoliday @p0, @p1, @p2, @p3",
                holiday.HolidayId, holiday.HolidayName, holiday.FromDate, holiday.ToDate);
        }

        public List<Holiday> GetHolidays()
        {
            return Holidays.SqlQuery("GetHolidays").ToList();
        }
    }

}