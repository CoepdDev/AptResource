using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AptEMS.DAL
{
    public class ThirdRound
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.
                       ConnectionStrings["cs"].ToString());

        public int AddThirdRound(Models.ThirdRound e1)
        {

            con.Open();
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.ThirdRound WHERE Email = @Email", con))
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
            SqlCommand cmd = new SqlCommand("sp_InsertThirdRound", con);
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
        public int AddNextRound(Models.SelectedCandidates e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_InsertSelected", con);
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

        public int DeleteThirdRound(Models.ThirdRound e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_DeleteThirdRound", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Mobile", e1.Mobile);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

        public int UpdateThirdRound(Models.ThirdRound e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateThirdRound", con);
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

        public List<Models.ThirdRound> GetThirdRound()
        {
            List<Models.ThirdRound> li = new List<Models.ThirdRound>();


            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetThirdRound", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Models.ThirdRound e1 = new Models.ThirdRound();
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

        public Models.ThirdRound SearchThirdRound(Models.ThirdRound e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchThirdRound", con);
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