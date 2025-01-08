using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
    public class DailyAttendance
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.
         ConnectionStrings["cs"].ToString());

        public int AddDailyAttendance(Models.DailyAttendance e1)
        {

            con.Open();
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.DailyAttendance WHERE Empid = @Empid", con))
            {
                checkCmd.Parameters.AddWithValue("@Empid", e1.Empid);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // ID already exists, return a value to indicate failure or handle as needed
                    con.Close();
                    return -1; // Indicating duplicate ID, or you can throw an exception or handle differently
                }
            }
            SqlCommand cmd = new SqlCommand("sp_InsertDailyAttendance", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@FirstName", e1.FirstName);
            cmd.Parameters.AddWithValue("@DOJ", e1.DOJ);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@Login", e1.Login);
            cmd.Parameters.AddWithValue("@Logout", e1.Logout);
       

            int i = cmd.ExecuteNonQuery();
            con.Close();


            return i;
        }

        public int DeleteDailyAttendance(Models.DailyAttendance e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_DeleteDailyAttendance", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

        public int UpdateDailyAttendance(Models.DailyAttendance e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateDailyAttendance", con);
            cmd.CommandType = CommandType.StoredProcedure;





            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@FirstName", e1.FirstName);
            cmd.Parameters.AddWithValue("@DOJ", e1.DOJ);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@Login", e1.Login);
            cmd.Parameters.AddWithValue("@Logout", e1.Logout);


            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }



        public List<Models.DailyAttendance> GetDailyAttendance()
        {
            List<Models.DailyAttendance> li = new List<Models.DailyAttendance>();


            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetDailyAttendance", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Models.DailyAttendance e1 = new Models.DailyAttendance();


                    e1.Empid = int.Parse(dr[0].ToString());
                    e1.FirstName = dr[1].ToString();
                    e1.DOJ = DateTime.Parse(dr[2].ToString());
                    e1.Designation = dr[3].ToString();
                    e1.Login = DateTime.Parse(dr[4].ToString());
                    e1.Logout = DateTime.Parse(dr[5].ToString());



                    li.Add(e1);
                }

            }
            con.Close();
            return li;


        }
        public Models.DailyAttendance SearchDailyAttendance(Models.DailyAttendance e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchDailyAttendance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    e1.FirstName = dr[0].ToString();
                    e1.DOJ = DateTime.Parse(dr[1].ToString());
                    e1.Designation = dr[2].ToString();
                    e1.Login = DateTime.Parse(dr[3].ToString());
                    e1.Logout = DateTime.Parse(dr[4].ToString());


                }
            }
            con.Close();
            return e1;

        }


    }
}