using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
    public class Incentives
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.
         ConnectionStrings["cs"].ToString());

        public int AddIncentives(Models.Incentives e1)
        {

            con.Open();
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Incentives WHERE Empid = @Empid", con))
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
            SqlCommand cmd = new SqlCommand("sp_InsertIncentive", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@FirstName", e1.FirstName);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@IncentiveType", e1.IncentiveType);
            cmd.Parameters.AddWithValue("@Amount", e1.Amount);
            cmd.Parameters.AddWithValue("@DateAwarded", e1.DateAwared);
            cmd.Parameters.AddWithValue("@Status", e1.Status);
            cmd.Parameters.AddWithValue("@PaymentDate", e1.PaymentDate);
            cmd.Parameters.AddWithValue("@Manager", e1.Manager);
            cmd.Parameters.AddWithValue("@Approval", e1.Approval);

            int i = cmd.ExecuteNonQuery();
            con.Close();


            return i;
        }

        public int DeleteIncentives(Models.Incentives e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_DeleteIncentive", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

        public int UpdateIncentives(Models.Incentives e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateIncentive", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@FirstName", e1.FirstName);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@IncentiveType", e1.IncentiveType);
            cmd.Parameters.AddWithValue("@Amount", e1.Amount);
            cmd.Parameters.AddWithValue("@DateAwarded", e1.DateAwared);
            cmd.Parameters.AddWithValue("@Status", e1.Status);
            cmd.Parameters.AddWithValue("@PaymentDate", e1.PaymentDate);
            cmd.Parameters.AddWithValue("@Manager", e1.Manager);
            cmd.Parameters.AddWithValue("@Approval", e1.Approval);


            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

        public List<Models.Incentives> GetIncentives()
        {
            List<Models.Incentives> li = new List<Models.Incentives>();


            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetAllIncentives", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Models.Incentives e1 = new Models.Incentives();
                    e1.Empid = int.Parse(dr[0].ToString());
                    e1.FirstName = dr[1].ToString();
                    e1.Designation = dr[2].ToString();
                    e1.IncentiveType = dr[3].ToString();
                    e1.Amount = dr[4].ToString();
                    e1.DateAwared = DateTime.Parse(dr[5].ToString());
                    e1.Status = dr[6].ToString();
                    e1.PaymentDate = DateTime.Parse(dr[7].ToString());
                    e1.Manager = dr[8].ToString();
                    e1.Approval = dr[9].ToString();

                    li.Add(e1);
                }

            }
            con.Close();
            return li;


        }
        public Models.Incentives SearchIncentives(Models.Incentives e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchIncentive", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    e1.FirstName = dr[0].ToString();
                    e1.Designation = dr[1].ToString();
                    e1.IncentiveType = dr[2].ToString();
                    e1.Amount = dr[3].ToString();
                    e1.DateAwared = DateTime.Parse(dr[4].ToString());
                    e1.Status = dr[5].ToString();
                    e1.PaymentDate = DateTime.Parse(dr[6].ToString());
                    e1.Manager = dr[7].ToString();
                    e1.Approval = dr[8].ToString();



                }
            }
            con.Close();
            return e1;

        }




    }
}