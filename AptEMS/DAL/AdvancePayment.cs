using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
    
    public class AdvancePayment
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.
              ConnectionStrings["cs"].ToString());
        public int Add(Models.AdvancePayment e1)
        {
            con.Open();

            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.AdvancePayment WHERE Empid = @Empid", con))
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
                SqlCommand cmd = new SqlCommand("sp_InsertAdvancePayment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@Empname", e1.Empname);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@AdvanceType", e1.AdvanceType);
            cmd.Parameters.AddWithValue("@AdvanceAmount", e1.AdvanceAmount);
            cmd.Parameters.AddWithValue("@RequestDate", e1.RequestDate);
            cmd.Parameters.AddWithValue("@ApprovalDate", e1.ApprovalDate);
            cmd.Parameters.AddWithValue("@RepaymentMonths", e1.RepaymentMonths);
            cmd.Parameters.AddWithValue("@RepaymentSchedule", e1.RepaymentSchedule);
            cmd.Parameters.AddWithValue("@RepaymentAmount", e1.RepaymentAmount);
            cmd.Parameters.AddWithValue("@DeductionStartDate", e1.DeductionStartDate);
            cmd.Parameters.AddWithValue("@DeductionEndDate", e1.DeductionEndDate);
            cmd.Parameters.AddWithValue("@Remarks", e1.Remarks);
           
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Delete(Models.AdvancePayment e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_DeleteAdvancePayment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Update(Models.AdvancePayment e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateAdvancePayment", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@Empname", e1.Empname);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@AdvanceType", e1.AdvanceType);
            cmd.Parameters.AddWithValue("@AdvanceAmount", e1.AdvanceAmount);
            cmd.Parameters.AddWithValue("@RequestDate", e1.RequestDate);
            cmd.Parameters.AddWithValue("@ApprovalDate", e1.ApprovalDate);
            cmd.Parameters.AddWithValue("@RepaymentMonths", e1.RepaymentMonths);
            cmd.Parameters.AddWithValue("@RepaymentSchedule", e1.RepaymentSchedule);
            cmd.Parameters.AddWithValue("@RepaymentAmount", e1.RepaymentAmount);
            cmd.Parameters.AddWithValue("@DeductionStartDate", e1.DeductionStartDate);
            cmd.Parameters.AddWithValue("@DeductionEndDate", e1.DeductionEndDate);
            cmd.Parameters.AddWithValue("@Remarks", e1.Remarks);



        
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Models.AdvancePayment> Get()
        {
            List<Models.AdvancePayment> li = new List<Models.AdvancePayment>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetAdvancePayment", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {



                while (dr.Read())
                {
                    Models.AdvancePayment e1 = new Models.AdvancePayment();
                    e1.Empid = int.Parse(dr[0].ToString());
                    e1.Empname = dr[1].ToString();
                    e1.Designation = dr[2].ToString();
                    e1.AdvanceType = dr[3].ToString();
                    e1.AdvanceAmount = decimal.Parse(dr[4].ToString());
                    e1.RequestDate = DateTime.Parse(dr[5].ToString());
                    e1.ApprovalDate = DateTime.Parse(dr[6].ToString());
                    e1.RepaymentMonths = int.Parse(dr[7].ToString());
                    e1.RepaymentSchedule = dr[8].ToString();
                    e1.RepaymentAmount = decimal.Parse(dr[9].ToString());
                    e1.DeductionStartDate = DateTime.Parse(dr[10].ToString());
                    e1.DeductionEndDate = DateTime.Parse(dr[11].ToString());
                    e1.Remarks = dr[12].ToString();


                    li.Add(e1);
                }
            }
            con.Close();
            return li;
        }
        public Models.AdvancePayment Search(Models.AdvancePayment e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchAdvancePayment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {

                    e1.Empname = dr[0].ToString();
                    e1.Designation = dr[1].ToString();
                    e1.AdvanceType = dr[2].ToString();
                    e1.AdvanceAmount = decimal.Parse(dr[3].ToString());
                    e1.RequestDate = DateTime.Parse(dr[4].ToString());
                    e1.ApprovalDate = DateTime.Parse(dr[5].ToString());
                    e1.RepaymentMonths = int.Parse(dr[6].ToString());
                    e1.RepaymentSchedule = dr[7].ToString();
                    e1.RepaymentAmount = decimal.Parse(dr[8].ToString());
                    e1.DeductionStartDate = DateTime.Parse(dr[9].ToString());
                    e1.DeductionEndDate = DateTime.Parse(dr[10].ToString());
                    e1.Remarks = dr[11].ToString();




                }
            }
            con.Close();
            return e1;
        }

    }
}