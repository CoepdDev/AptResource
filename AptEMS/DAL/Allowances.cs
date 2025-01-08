using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
  
    public class Allowances
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.
            ConnectionStrings["cs"].ToString());
        public int Add(Models.Allowances e1)
        {
            con.Open();

            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Allowances WHERE Empid = @Empid", con))
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
            SqlCommand cmd = new SqlCommand("InsertAllowance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@Empname", e1.Empname);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@AllowanceType", e1.AllowanceType);
            cmd.Parameters.AddWithValue("@AllowanceAmount", e1.AllowanceAmount);
            cmd.Parameters.AddWithValue("@Frequency", e1.Frequency);
            cmd.Parameters.AddWithValue("@EffectiveDate", e1.EffectiveDate);
            cmd.Parameters.AddWithValue("@EndDate", e1.EndDate);
            cmd.Parameters.AddWithValue("@Reason", e1.Reason);
            cmd.Parameters.AddWithValue("@Status", e1.Status);
          
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Delete(Models.Allowances e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DeleteAllowance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Update(Models.Allowances e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("EditAllowance", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@Empname", e1.Empname);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@AllowanceType", e1.AllowanceType);
            cmd.Parameters.AddWithValue("@AllowanceAmount", e1.AllowanceAmount);
            cmd.Parameters.AddWithValue("@Frequency", e1.Frequency);
            cmd.Parameters.AddWithValue("@EffectiveDate", e1.EffectiveDate);
            cmd.Parameters.AddWithValue("@EndDate", e1.EndDate);
            cmd.Parameters.AddWithValue("@Reason", e1.Reason);
            cmd.Parameters.AddWithValue("@Status", e1.Status);


   
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Models.Allowances> Get()
        {
            List<Models.Allowances> li = new List<Models.Allowances>();
            con.Open();
            SqlCommand cmd = new SqlCommand("GetAllowance", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {



                while (dr.Read())
                {
                    Models.Allowances e1 = new Models.Allowances();
                    e1.Empid = int.Parse(dr[0].ToString());
                    e1.Empname = dr[1].ToString();
                    e1.Designation = dr[2].ToString();
                    int allowanceType;
                    if (!int.TryParse(dr[3].ToString(), out allowanceType))
                    {
                        // Handle the case where parsing fails (e.g., log an error, set a default value, etc.)
                        allowanceType = 0;  // Or any other default value
                    }
                    e1.AllowanceType = allowanceType;

                    e1.AllowanceAmount = int.Parse(dr[4].ToString());
                    e1.Frequency = int.Parse(dr[5].ToString());
                    DateTime effectiveDate;
                    if (DateTime.TryParse(dr[6].ToString(), out effectiveDate))
                    {
                        e1.EffectiveDate = effectiveDate;
                    }
                    else
                    {
                        // Handle invalid date format (e.g., set a default value, log an error, etc.)
                        e1.EffectiveDate = DateTime.MinValue; // Default value or handle appropriately
                    }

                    e1.EndDate = DateTime.Parse(dr[7].ToString());
                    e1.Reason = dr[8].ToString();
                    e1.Status = dr[9].ToString();
                    li.Add(e1);
                }
            }
            con.Close();
            return li;
        }
        public Models.Allowances Search(Models.Allowances e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SearchAllowances", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {


                    e1.Empname = dr[0].ToString();
                    e1.Designation = dr[1].ToString();
                    e1.AllowanceType = int.Parse(dr[2].ToString());
                    e1.AllowanceAmount = int.Parse(dr[3].ToString());
                    e1.Frequency = int.Parse(dr[4].ToString());
                    e1.EffectiveDate = DateTime.Parse(dr[5].ToString());
                    e1.EndDate = DateTime.Parse(dr[6].ToString());
                    e1.Reason = dr[7].ToString();
                    e1.Status = dr[8].ToString();


                }
            }
            con.Close();
            return e1;
        }
    }
}