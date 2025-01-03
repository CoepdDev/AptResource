using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
    public class SalaryTranscation
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.
ConnectionStrings["cs"].ToString());



        public int Addsalary(Models.SalaryTranscation e1)
        {
            con.Open();

            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.SalaryTranscation WHERE Empid = @Empid", con))
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
            SqlCommand cmd = new SqlCommand("sp_InsertSalaryTransaction", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@Empname", e1.FirstName);
            cmd.Parameters.AddWithValue("@BankAccountNumber", e1.BankAccountNumber);
            cmd.Parameters.AddWithValue("@Bankname", e1.BankName);
            cmd.Parameters.AddWithValue("@Leaves", e1.Leaves);
            cmd.Parameters.AddWithValue("@LOP", e1.LOP);
            cmd.Parameters.AddWithValue("@LoanAmount", e1.LoanAmount);
            cmd.Parameters.AddWithValue("@Workingdays", e1.WorkingDays);
            cmd.Parameters.AddWithValue("@Netsalary", e1.Netsalary);
            cmd.Parameters.AddWithValue("@ApprisialPoints", e1.ApprisialPoints);
            cmd.Parameters.AddWithValue("@WarnigPoints", e1.WarnigPoints);
            cmd.Parameters.AddWithValue("@DelayPoints", e1.DelayPoints);
            cmd.Parameters.AddWithValue("@Bonus", e1.Bonus);
            cmd.Parameters.AddWithValue("@Allownaces", e1.Allowances);
            cmd.Parameters.AddWithValue("@Transcationdate", e1.TransactionDate);
            cmd.Parameters.AddWithValue("@SourceBank", e1.SourceBank);
            cmd.Parameters.AddWithValue("@CreditedBy", e1.CreditedBy);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Deletesalary(Models.SalaryTranscation e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_DeleteSalaryTransaction", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Updatesalary(Models.SalaryTranscation e1)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateSalaryTransaction", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@Empname", e1.FirstName);
            cmd.Parameters.AddWithValue("@BankAccountNumber", e1.BankAccountNumber);
            cmd.Parameters.AddWithValue("@Bankname", e1.BankName);
            cmd.Parameters.AddWithValue("@Leaves", e1.Leaves);
            cmd.Parameters.AddWithValue("@LOP", e1.LOP);
            cmd.Parameters.AddWithValue("@LoanAmount", e1.LoanAmount);
            cmd.Parameters.AddWithValue("@Workingdays", e1.WorkingDays);
            cmd.Parameters.AddWithValue("@Netsalary", e1.Netsalary);
            cmd.Parameters.AddWithValue("@ApprisialPoints", e1.ApprisialPoints);
            cmd.Parameters.AddWithValue("@WarnigPoints", e1.WarnigPoints);
            cmd.Parameters.AddWithValue("@DelayPoints", e1.DelayPoints);
            cmd.Parameters.AddWithValue("@Bonus", e1.Bonus);
            cmd.Parameters.AddWithValue("@Allownaces", e1.Allowances);
            cmd.Parameters.AddWithValue("@Transcationdate", e1.TransactionDate);
            cmd.Parameters.AddWithValue("@SourceBank", e1.SourceBank);
            cmd.Parameters.AddWithValue("@CreditedBy", e1.CreditedBy);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Models.SalaryTranscation> Getsalary()
        {
            List<Models.SalaryTranscation> li = new List<Models.SalaryTranscation>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetSalaryTransaction", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {


                while (dr.Read())
                {
                    Models.SalaryTranscation e1 = new Models.SalaryTranscation();
                    e1.Empid = int.Parse(dr[0].ToString());
                    e1.FirstName = dr[1].ToString();
                    e1.BankAccountNumber = dr[2].ToString();
                    e1.BankName = dr[3].ToString();
                    e1.Leaves = dr[4].ToString();
                    e1.LOP = dr[5].ToString();
                    e1.LoanAmount = dr[6].ToString();
                    e1.WorkingDays = dr[7].ToString();                
                    e1.Netsalary = dr[8].ToString();
                    e1.ApprisialPoints = dr[9].ToString();                  
                    e1.WarnigPoints = dr[10].ToString(); 
                    e1.DelayPoints = dr[11].ToString();
                    e1.Bonus = dr[12].ToString();                         
                    e1.Allowances = dr[13].ToString(); 
                    e1.TransactionDate = DateTime.TryParse(dr[14].ToString(), out DateTime doj) ? doj : DateTime.MinValue;                   
                    e1.SourceBank = dr[15].ToString(); 
                    e1.CreditedBy = dr[16].ToString();
                  

                    li.Add(e1);
                }
            }
            con.Close();
            return li;
        }
        public Models.SalaryTranscation Searchsalary(Models.SalaryTranscation e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchSalaryTransaction", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    e1.FirstName = dr[0].ToString();
                    e1.BankAccountNumber = dr[1].ToString();
                    e1.BankName = dr[2].ToString();
                    e1.Leaves = dr[3].ToString();
                    e1.LOP = dr[4].ToString();
                    e1.LoanAmount = dr[5].ToString();
                    e1.WorkingDays = dr[6].ToString();
                    e1.Netsalary = dr[7].ToString();
                    e1.ApprisialPoints = dr[8].ToString();
                    e1.WarnigPoints = dr[9].ToString();
                    e1.DelayPoints = dr[10].ToString();
                    e1.Bonus = dr[11].ToString();
                    e1.Allowances = dr[12].ToString();
                    e1.TransactionDate = DateTime.TryParse(dr[13].ToString(), out DateTime doj) ? doj : DateTime.MinValue;
                    e1.SourceBank = dr[14].ToString();
                    e1.CreditedBy = dr[15].ToString();

                }
            }
            con.Close();
            return e1;
        }











    }
}