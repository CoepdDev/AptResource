using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{

    public class Employee
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.
     ConnectionStrings["cs"].ToString());
        public int Addemp(Models.Employee e1)
        {
            con.Open();

            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Employee WHERE Id = @Id", con))
            {
                checkCmd.Parameters.AddWithValue("@Id", e1.Id);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // ID already exists, return a value to indicate failure or handle as needed
                    con.Close();
                    return -1; // Indicating duplicate ID, or you can throw an exception or handle differently
                }
            }
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
        public int Deleteemp(Models.Employee e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", e1.Id);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Updateemp(Models.Employee e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", con);
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
            cmd.Parameters.AddWithValue("@BankIFSCcode", e1.BankIFSCCode);
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
        public List<Models.Employee> Getemp()
        {
            List<Models.Employee> li = new List<Models.Employee>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetAllEmployee", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

             

                while (dr.Read())
                {
                    Models.Employee e1 = new Models.Employee();
                    e1.Id = int.Parse(dr[0].ToString());
                    e1.FirstName = dr[1].ToString();
                    e1.LastName = dr[2].ToString();
                    e1.PermanentAddress = dr[3].ToString();
                    e1.CurrentAddress = dr[4].ToString();
                    e1.City = dr[5].ToString();
                    e1.State = dr[6].ToString();
                    //e1.PostaZipCode = int.Parse(dr[7].ToString());
                    e1.PostalZipCode = dr[7].ToString();
                    e1.Country = dr[8].ToString();
                    //e1.Phonenumber = int.Parse(dr[9].ToString());

                    e1.PhoneNumber = dr[9].ToString(); // Default to 0 if parsing fails
                    e1.PersonalEmailID = dr[10].ToString();
                    e1.OfficeEmailID = dr[11].ToString();
                    e1.BankName = dr[12].ToString();
                    //e1.Bankaccountnumber= int.Parse(dr[13].ToString());
                    e1.BankAccountNumber = dr[13].ToString(); // Default to 0 if parsing fails
                    e1.BankIFSCCode = dr[14].ToString();
                    //e1.DOJ =  DateTime.Parse(dr[15].ToString());
                    e1.DOJ = DateTime.TryParse(dr[15].ToString(), out DateTime doj) ? doj : DateTime.MinValue; // Default to MinValue if parsing fails
                    e1.Branch = dr[16].ToString();
                    e1.Designation = dr[17].ToString();
                    e1.Summary = dr[18].ToString();
                    e1.DOB = DateTime.TryParse(dr[19].ToString(), out DateTime dob) ? dob : DateTime.MinValue;
                    e1.Empphoto = dr[20].ToString();
                    e1.PANno = dr[21].ToString();
                    e1.PANphoto = dr[22].ToString();
                    e1.Aadharno = dr[23].ToString();    
                    e1.Aadharphoto = dr[24].ToString();
                    e1.isactive = bool.TryParse(dr[25].ToString(), out bool result) && result;


                    li.Add(e1);
                }
            }
            con.Close();
            return li;
        }
        public Models.Employee Searchemp(Models.Employee e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", e1.Id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    e1.FirstName = dr[0].ToString();
                    e1.LastName = dr[1].ToString();
                    e1.PermanentAddress = dr[2].ToString();
                    e1.CurrentAddress = dr[3].ToString();
                    e1.City = dr[4].ToString();
                    e1.State = dr[5].ToString();
                    e1.PostalZipCode = dr[6].ToString();
                    e1.Country = dr[7].ToString();
                    e1.PhoneNumber = dr[8].ToString();
                    e1.PersonalEmailID = dr[9].ToString();
                    e1.OfficeEmailID = dr[10].ToString();
                    e1.BankName = dr[11].ToString();
                    e1.BankAccountNumber = dr[12].ToString();
                    e1.BankIFSCCode = dr[13].ToString();
                    e1.DOJ = DateTime.Parse(dr[14].ToString());
                    e1.Branch = dr[15].ToString();
                    e1.Designation = dr[16].ToString();
                    e1.Summary = dr[17].ToString();
                    e1.DOB = DateTime.TryParse(dr[18].ToString(), out DateTime dob) ? dob : DateTime.MinValue;
                    e1.Empphoto = dr[19].ToString();
                    e1.PANno = dr[20].ToString();
                    e1.PANphoto = dr[21].ToString();
                    e1.Aadharno = dr[22].ToString();
                    e1.Aadharphoto = dr[23].ToString();
                    e1.isactive = bool.TryParse(dr[24].ToString(), out bool result) && result;
                }
            }
            con.Close();
            return e1;
        }

    }
}