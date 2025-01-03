using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
    public class AttendanceConfigure
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.
            ConnectionStrings["cs"].ToString());
        public int AddAttendanceConfigure(Models.AttendanceConfigure e1)
        {
            con.Open();

            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.AttendanceConfigure WHERE CreatedBy = @CreatedBy", con))
            {
                checkCmd.Parameters.AddWithValue("@CreatedBy", e1.CreatedBy);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // ID already exists, return a value to indicate failure or handle as needed
                    con.Close();
                    return -1; // Indicating duplicate ID, or you can throw an exception or handle differently
                }
            }
            SqlCommand cmd = new SqlCommand("sp_InsertAttendanceConfigure", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CreatedBy", e1.CreatedBy);
            cmd.Parameters.AddWithValue("@MonthName", e1.MonthName);
            cmd.Parameters.AddWithValue("@WorkingDays", e1.WorkingDays);
            cmd.Parameters.AddWithValue("@CreatedDate", e1.CreatedDate);
     

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int DeleteAttendanceConfigure(Models.AttendanceConfigure e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_DeleteAttendanceConfigure", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CreatedBy", e1.CreatedBy);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int UpdateAttendanceConfigure(Models.AttendanceConfigure e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateAttendanceConfigure", con);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.AddWithValue("@CreatedBy", e1.CreatedBy);
            cmd.Parameters.AddWithValue("@MonthName", e1.MonthName);
            cmd.Parameters.AddWithValue("@WorkingDays", e1.WorkingDays);
            cmd.Parameters.AddWithValue("@CreatedDate", e1.CreatedDate);



            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Models.AttendanceConfigure> GetAttendanceConfigure()
        {
            List<Models.AttendanceConfigure> li = new List<Models.AttendanceConfigure>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetAllAttendanceConfigure", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {



                while (dr.Read())
                {
                    Models.AttendanceConfigure e1 = new Models.AttendanceConfigure();
                    e1.CreatedBy = dr[0].ToString();
                    e1.MonthName = dr[1].ToString();
                    e1.WorkingDays = dr[2].ToString();
                    e1.CreatedDate = DateTime.Parse(dr[3].ToString());
              


                    li.Add(e1);
                }
            }
            con.Close();
            return li;
        }
        public Models.AttendanceConfigure SearchAttendanceConfigure(Models.AttendanceConfigure e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchAttendanceConfigure", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CreatedBy", e1.CreatedBy);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {


              
                    e1.MonthName = dr[0].ToString();
                    e1.WorkingDays = dr[1].ToString();
                    e1.CreatedDate = DateTime.Parse(dr[2].ToString());


                }
            }
            con.Close();
            return e1;
        }







    }
}