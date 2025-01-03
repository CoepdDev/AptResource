using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
  
    public class SalaryComparison
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.
              ConnectionStrings["cs"].ToString());
        public int Add(Models.SalaryComparison e1)
        {
            con.Open();

            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.SalaryComparison WHERE Empid = @Empid", con))
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
            SqlCommand cmd = new SqlCommand("InsertSalaryComparison", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@Empname", e1.Empname);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@NetSalary", e1.NetSalary);
            cmd.Parameters.AddWithValue("@PreviousMonthSalary", e1.PreviousMonthSalary);
            cmd.Parameters.AddWithValue("@ThisMonthSalary", e1.ThisMonthSalary);
            cmd.Parameters.AddWithValue("@SalaryChange", e1.SalaryChange);
            cmd.Parameters.AddWithValue("@Reason", e1.Reason);
            cmd.Parameters.AddWithValue("@Handledby", e1.Handledby);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Delete(Models.SalaryComparison e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DeleteSalaryComparison", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Update(Models.SalaryComparison e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("EditSalaryComparison", con);
            cmd.CommandType = CommandType.StoredProcedure;


           
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@Empname", e1.Empname);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@NetSalary", e1.NetSalary);
            cmd.Parameters.AddWithValue("@PreviousMonthSalary", e1.PreviousMonthSalary);
            cmd.Parameters.AddWithValue("@ThisMonthSalary", e1.ThisMonthSalary);
            cmd.Parameters.AddWithValue("@SalaryChange", e1.SalaryChange);
            cmd.Parameters.AddWithValue("@Reason", e1.Reason);
            cmd.Parameters.AddWithValue("@Handledby", e1.Handledby);



            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Models.SalaryComparison> Get()
        {
            List<Models.SalaryComparison> li = new List<Models.SalaryComparison>();
            con.Open();
            SqlCommand cmd = new SqlCommand("GetAllSalaryComparisons", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {



                while (dr.Read())
                {
                    Models.SalaryComparison e1 = new Models.SalaryComparison();
                    e1.Empid = int.Parse(dr[0].ToString());
                    e1.Empname = dr[1].ToString();
                    e1.Designation = dr[2].ToString();
                    e1.NetSalary = int.Parse(dr[3].ToString());
                    e1.PreviousMonthSalary = int.Parse(dr[4].ToString()); 
                    e1.ThisMonthSalary = int.Parse(dr[5].ToString());
                    e1.SalaryChange = int.Parse(dr[6].ToString());
                    e1.Reason = dr[7].ToString();
                    e1.Handledby = int.Parse(dr[8].ToString());


                    li.Add(e1);
                }
            }
            con.Close();
            return li;
        }
        public Models.SalaryComparison Search(Models.SalaryComparison e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SearchSalaryComparisons", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {


                    e1.Empname = dr[0].ToString();
                    e1.Designation = dr[1].ToString();
                    e1.NetSalary = int.Parse(dr[2].ToString());
                    e1.PreviousMonthSalary = int.Parse(dr[3].ToString());
                    e1.ThisMonthSalary = int.Parse(dr[4].ToString());
                    e1.SalaryChange = int.Parse(dr[5].ToString());
                    e1.Reason = dr[6].ToString();
                    e1.Handledby = int.Parse(dr[7].ToString());


                }
            }
            con.Close();
            return e1;
        }
    }
}