using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
    public class OfferLetterReleased
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.
               ConnectionStrings["cs"].ToString());

        public int AddOfferLetterReleased(Models.OfferLetterReleased e1)
        {

            con.Open();
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.OfferLetterReleased WHERE Email = @Email", con))
            {
                checkCmd.Parameters.AddWithValue("@Email", e1.Email);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // ID already exists, return a value to indicate failure or handle as needed
                    con.Close();
                    return -1; // Indicating duplicate ID, or you can throw an exception or handle differently
                }
            }
            SqlCommand cmd = new SqlCommand("sp_InsertOfferLetterReleased", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Name", e1.Name);
            cmd.Parameters.AddWithValue("@Role", e1.Role);
            cmd.Parameters.AddWithValue("@InterviewerName", e1.InterviewerName);
            cmd.Parameters.AddWithValue("@Date", e1.Date);
            cmd.Parameters.AddWithValue("@Remark", e1.Remark);
            cmd.Parameters.AddWithValue("@Mobile", e1.Mobile);
            cmd.Parameters.AddWithValue("@Email", e1.Email);



            int i = cmd.ExecuteNonQuery();
            con.Close();


            return i;
        }
        public int Addemp(Models.Employee e1)
        {
            con.Open();


            SqlCommand cmd = new SqlCommand("sp_InsertEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", e1.Id);
            cmd.Parameters.AddWithValue("@FirstName", e1.FirstName);
            cmd.Parameters.AddWithValue("@LastName", e1.LastName);
            cmd.Parameters.AddWithValue("@PermanentAddress", e1.PermanentAddress);
            cmd.Parameters.AddWithValue("@CurrentAddress", e1.CurrentAddress);
            cmd.Parameters.AddWithValue("@City", e1.City);
            cmd.Parameters.AddWithValue("@State", e1.State);
            cmd.Parameters.AddWithValue("@PostalZipCode", e1.PostalZipCode);
            cmd.Parameters.AddWithValue("@Country", e1.Country);
            cmd.Parameters.AddWithValue("@Phonenumber", e1.PhoneNumber);
            cmd.Parameters.AddWithValue("@PersonalEmailID", e1.PersonalEmailID);
            cmd.Parameters.AddWithValue("@OfficeEmailID", e1.OfficeEmailID);
            cmd.Parameters.AddWithValue("@BankName", e1.BankName);
            cmd.Parameters.AddWithValue("@BankAccountNumber", e1.BankAccountNumber);
            cmd.Parameters.AddWithValue("@BankIFSCCode", e1.BankIFSCCode);
            cmd.Parameters.AddWithValue("@DOJ", e1.DOJ);
            cmd.Parameters.AddWithValue("@Branch", e1.Branch);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@Summary", e1.Summary);
            cmd.Parameters.AddWithValue("@DOB", e1.DOB);
            cmd.Parameters.AddWithValue("@Empphoto", e1.Empphoto);
            cmd.Parameters.AddWithValue("@PANno", e1.PANno);
            cmd.Parameters.AddWithValue("@PANphoto", e1.PANphoto);
            cmd.Parameters.AddWithValue("@Aadharno", e1.Aadharno);
            cmd.Parameters.AddWithValue("@Aadharphoto", e1.Aadharphoto);
            cmd.Parameters.AddWithValue("@isactive", e1.isactive);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }



        public int DeleteOfferLetterReleased(Models.OfferLetterReleased e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_DeleteOfferLetterReleased", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Mobile", e1.Mobile);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

        public int UpdateOfferLetterReleased(Models.OfferLetterReleased e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateOfferLetterReleased", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Name", e1.Name);
            cmd.Parameters.AddWithValue("@Role", e1.Role);
            cmd.Parameters.AddWithValue("@InterviewerName", e1.InterviewerName);
            cmd.Parameters.AddWithValue("@Date", e1.Date);
            cmd.Parameters.AddWithValue("@Remark", e1.Remark);
            cmd.Parameters.AddWithValue("@Mobile", e1.Mobile);
            cmd.Parameters.AddWithValue("@Email", e1.Email);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

        public List<Models.OfferLetterReleased> GetOfferLetterReleased()
        {
            List<Models.OfferLetterReleased> li = new List<Models.OfferLetterReleased>();


            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetOfferLetterReleased", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Models.OfferLetterReleased e1 = new Models.OfferLetterReleased();
                    e1.Name = dr[0].ToString();
                    e1.Role = dr[1].ToString();
                    e1.InterviewerName = dr[2].ToString();
                    e1.Date = DateTime.Parse(dr[3].ToString());
                    e1.Remark = dr[4].ToString();
                    e1.Mobile = dr[5].ToString();
                    e1.Email = dr[6].ToString();

                    li.Add(e1);
                }

            }
            con.Close();
            return li;


        }

        public Models.OfferLetterReleased SearchOfferLetterReleased(Models.OfferLetterReleased e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchOfferLetterReleased", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mobile", e1.Mobile);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    e1.Name = dr[0].ToString();
                    e1.Role = dr[1].ToString();
                    e1.InterviewerName = dr[2].ToString();
                    e1.Date = DateTime.Parse(dr[3].ToString());
                    e1.Remark = dr[4].ToString();
                    e1.Email = dr[5].ToString();



                }
            }
            con.Close();
            return e1;

        }
    }
}