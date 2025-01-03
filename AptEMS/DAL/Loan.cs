using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
    public class Loan
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.
         ConnectionStrings["cs"].ToString());

        public int AddLoan(Models.Loan e1)
        {

            con.Open();
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Loan WHERE Empid = @Empid", con))
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
            SqlCommand cmd = new SqlCommand("sp_InsertLoan", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@FirstName", e1.FirstName);
            cmd.Parameters.AddWithValue("@SalaryTransactionID", e1.SalaryTransactionID);
            cmd.Parameters.AddWithValue("@ApprovalDate", e1.ApprovalDate);
            cmd.Parameters.AddWithValue("@LoanSanctionAmount", e1.LoanSanctionAmount);
            cmd.Parameters.AddWithValue("@Tenure", e1.Tenure);
            cmd.Parameters.AddWithValue("@EMIAmount", e1.EMIAmount);
            cmd.Parameters.AddWithValue("@LoanSourceBank", e1.LoanSourceBank);
            cmd.Parameters.AddWithValue("@LoanCreditedBy", e1.LoanCreditedBy);
         

            int i = cmd.ExecuteNonQuery();
            con.Close();


            return i;
        }

        public int DeleteLoan(Models.Loan e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_DeleteLoan", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

        public int UpdateLoan(Models.Loan e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateLoan", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@FirstName", e1.FirstName);
            cmd.Parameters.AddWithValue("@SalaryTransactionID", e1.SalaryTransactionID);
            cmd.Parameters.AddWithValue("@ApprovalDate", e1.ApprovalDate);
            cmd.Parameters.AddWithValue("@LoanSanctionAmount", e1.LoanSanctionAmount);
            cmd.Parameters.AddWithValue("@Tenure", e1.Tenure);
            cmd.Parameters.AddWithValue("@EMIAmount", e1.EMIAmount);
            cmd.Parameters.AddWithValue("@LoanSourceBank", e1.LoanSourceBank);
            cmd.Parameters.AddWithValue("@LoanCreditedBy", e1.LoanCreditedBy);



            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

        public List<Models.Loan> GetLoan()
        {
            List<Models.Loan> li = new List<Models.Loan>();


            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetLoan", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Models.Loan e1 = new Models.Loan    ();
                    e1.Empid = int.Parse(dr[0].ToString());
                    e1.FirstName = dr[1].ToString();
                    e1.SalaryTransactionID = dr[2].ToString();
                    e1.ApprovalDate = DateTime.Parse(dr[3].ToString());
                    e1.LoanSanctionAmount = dr[4].ToString();
                    e1.Tenure = dr[5].ToString();
                    e1.EMIAmount = dr[6].ToString();
                    e1.LoanSourceBank = dr[7].ToString();
                    e1.LoanCreditedBy = dr[8].ToString();
        

                    li.Add(e1);
                }

            }
            con.Close();
            return li;


        }
        public Models.Loan SearchLoan(Models.Loan e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchLoan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    e1.FirstName = dr[0].ToString();
                    e1.SalaryTransactionID = dr[1].ToString();
                    e1.ApprovalDate = DateTime.Parse(dr[2].ToString());
                    e1.LoanSanctionAmount = dr[3].ToString();
                    e1.Tenure =dr[4].ToString();
                    e1.EMIAmount = dr[5].ToString();
                    e1.LoanSourceBank = dr[6].ToString();
                    e1.LoanCreditedBy = dr[7].ToString();



                }
            }
            con.Close();
            return e1;

        }




    }
}