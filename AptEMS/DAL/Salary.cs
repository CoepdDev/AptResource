using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
    public class Salary
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.
             ConnectionStrings["cs"].ToString());


    
        public int AddSalary(Models.Salary e1)
        {

            con.Open();
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Salary WHERE Empid = @Empid", con))
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
            SqlCommand cmd = new SqlCommand("sp_AddSalary", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@Empname", e1.Empname);
            cmd.Parameters.AddWithValue("@Credited", e1.Credited);
            cmd.Parameters.AddWithValue("@Empsalary", e1.Empsalary);
            cmd.Parameters.AddWithValue("@Deductions", e1.Deductions);
            cmd.Parameters.AddWithValue("@Hike", e1.Hike);
            cmd.Parameters.AddWithValue("@Newsalary", e1.Newsalary);

            int i = cmd.ExecuteNonQuery();
            con.Close();


            return i;
        }

        public int DeleteSalary(Models.Salary e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_DeleteSalary", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }
        public int UpdateSalary(Models.Salary e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateSalary", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@Empname", e1.Empname);
            cmd.Parameters.AddWithValue("@Credited", e1.Credited);
            cmd.Parameters.AddWithValue("@Empsalary", e1.Empsalary);
            cmd.Parameters.AddWithValue("@Deductions", e1.Deductions);
            cmd.Parameters.AddWithValue("@Hike", e1.Hike);
            cmd.Parameters.AddWithValue("@Newsalary", e1.Newsalary);

          
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

        public List<Models.Salary> GetSalary()
        {
            List<Models.Salary> li = new List<Models.Salary>();


            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetAllSalaries", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Models.Salary e1 = new Models.Salary();
                    e1.Empid = int.Parse(dr[0].ToString());
                    e1.Empname = dr[1].ToString();
                    e1.Credited = DateTime.Parse(dr[2].ToString());
                    e1.Empsalary = decimal.Parse(dr[3].ToString());
                    e1.Deductions = decimal.Parse(dr[4].ToString());
                    e1.Hike = decimal.Parse(dr[5].ToString());
                    e1.Newsalary = decimal.Parse(dr[6].ToString());

                    li.Add(e1);
                }

            }
            con.Close();
            return li;


        }

        public Models.Salary SearchSalary(Models.Salary e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchSalary", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                  
                    e1.Empname = dr[0].ToString();
                    e1.Credited = DateTime.Parse(dr[1].ToString());
                    e1.Empsalary = decimal.Parse(dr[2].ToString());
                    e1.Deductions = decimal.Parse(dr[3].ToString());
                    e1.Hike = decimal.Parse(dr[4].ToString());
                    e1.Newsalary = decimal.Parse(dr[5].ToString());

                }
            }
            con.Close();
            return e1;

        }














    }
}