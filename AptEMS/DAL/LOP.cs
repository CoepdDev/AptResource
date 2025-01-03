using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
    public class LOP
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.
         ConnectionStrings["cs"].ToString());


        public int AddLOP(Models.LOP e1)
        {

            con.Open();
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.LOP WHERE Empid = @Empid", con))
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
            SqlCommand cmd = new SqlCommand("sp_InsertLOP", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@FirstName", e1.FirstName);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@LeaveType", e1.LeaveType);
            cmd.Parameters.AddWithValue("@LeaveStart", e1.LeaveStart);
            cmd.Parameters.AddWithValue("@LeaveEnd", e1.LeaveEnd);
            cmd.Parameters.AddWithValue("@TotalLeaveDays", e1.TotalLeaveDays);
            cmd.Parameters.AddWithValue("@Reason", e1.Reason);
            cmd.Parameters.AddWithValue("@LeaveStatus", e1.LeaveStatus);
            cmd.Parameters.AddWithValue("@LeaveLeft", e1.LeaveLeft);

            int i = cmd.ExecuteNonQuery();
            con.Close();


            return i;
        }

        public int DeleteLOP(Models.LOP e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_DeleteLOP", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }
        public int UpdateLOP(Models.LOP e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateLOP", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@FirstName", e1.FirstName);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@LeaveType", e1.LeaveType);
            cmd.Parameters.AddWithValue("@LeaveStart", e1.LeaveStart);
            cmd.Parameters.AddWithValue("@LeaveEnd", e1.LeaveEnd);
            cmd.Parameters.AddWithValue("@TotalLeaveDays", e1.TotalLeaveDays);
            cmd.Parameters.AddWithValue("@Reason", e1.Reason);
            cmd.Parameters.AddWithValue("@LeaveStatus", e1.LeaveStatus);
            cmd.Parameters.AddWithValue("@LeaveLeft", e1.LeaveLeft);



            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

        public List<Models.LOP> GetLOP()
        {
            List<Models.LOP> li = new List<Models.LOP>();


            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetLOP", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Models.LOP e1 = new Models.LOP();
                    e1.Empid = int.Parse(dr[0].ToString());
                    e1.FirstName = dr[1].ToString();
                    e1.Designation = dr[2].ToString();
                    e1.LeaveType = dr[3].ToString();
                    e1.LeaveStart = DateTime.Parse(dr[4].ToString());
                    e1.LeaveEnd = DateTime.Parse(dr[5].ToString());
                    e1.TotalLeaveDays = dr[6].ToString();
                    e1.Reason = dr[7].ToString();
                    e1.LeaveStatus = dr[8].ToString();
                    e1.LeaveLeft = dr[9].ToString();

                    li.Add(e1);
                }

            }
            con.Close();
            return li;


        }
        public Models.LOP SearchLOP(Models.LOP e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchLOP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {

                    e1.FirstName = dr[0].ToString();
                    e1.Designation = dr[1].ToString();
                    e1.LeaveType = dr[2].ToString();
                    e1.LeaveStart = DateTime.Parse(dr[3].ToString());
                    e1.LeaveEnd = DateTime.Parse(dr[4].ToString());
                    e1.TotalLeaveDays = dr[5].ToString();
                    e1.Reason = dr[6].ToString();
                    e1.LeaveStatus = dr[7].ToString();
                    e1.LeaveLeft = dr[8].ToString();


                }
            }
            con.Close();
            return e1;

        }









    }
}