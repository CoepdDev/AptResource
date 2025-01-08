using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Reflection;

namespace AptEMS.DAL
{
    public class shiftreg
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.
             ConnectionStrings["cs"].ToString());

        public int Addshiftreg(Models.Shiftreg e1)
        {

            con.Open();
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.shiftreg WHERE s_type = @s_type", con))
            {
                checkCmd.Parameters.AddWithValue("@s_type", e1.s_type);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // ID already exists, return a value to indicate failure or handle as needed
                    con.Close();
                    return -1; // Indicating duplicate ID, or you can throw an exception or handle differently
                }
            }
            SqlCommand cmd = new SqlCommand("pro_addshiftreg", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@s_type", e1.s_type);
            cmd.Parameters.AddWithValue("@s_start", e1.s_end);
            cmd.Parameters.AddWithValue("@s_end", e1.s_start);
            cmd.Parameters.AddWithValue("@brk", e1.brk);
            cmd.Parameters.AddWithValue("@attendence", e1.attendence);
            int i = cmd.ExecuteNonQuery();
            con.Close();


            return i;
        }
        public int Deleteshiftreg(Models.Shiftreg e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("pro_deleteshiftreg", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@s_type", e1.s_type);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }
        public int Updateshiftreg(Models.Shiftreg e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("pro_updateshiftreg", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@s_type", e1.s_type);
            cmd.Parameters.AddWithValue("@s_start", e1.s_start);
            cmd.Parameters.AddWithValue("@s_end", e1.s_end);
            cmd.Parameters.AddWithValue("@brk", e1.brk);
            cmd.Parameters.AddWithValue("@attendence", e1.attendence);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }
        public List<Models.Shiftreg> Getshiftreg()
        {
            List<Models.Shiftreg> li = new List<Models.Shiftreg>();


            con.Open();
            SqlCommand cmd = new SqlCommand("pro_getshiftreg", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Models.Shiftreg e1 = new Models.Shiftreg();

                    // Check for null values before assignment
                    e1.s_type = dr[0] != DBNull.Value ? dr[0].ToString() : null;
                    e1.s_start = dr[1] != DBNull.Value ? DateTime.Parse(dr[1].ToString()) : (DateTime?)null;
                    e1.s_end = dr[2] != DBNull.Value ? DateTime.Parse(dr[2].ToString()) : (DateTime?)null;
                    e1.brk = dr[3] != DBNull.Value ? DateTime.Parse(dr[3].ToString()) : (DateTime?)null;
                    e1.attendence = dr[4] != DBNull.Value ? dr[4].ToString() : null;

                    li.Add(e1);
                }
            }

            con.Close();
            return li;
        }
        public Models.Shiftreg Searchshiftreg(Models.Shiftreg e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("pro_searchshiftreg", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@s_type", e1.s_type);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {

                    e1.s_start = DateTime.Parse(dr[0].ToString());
                    e1.s_end = DateTime.Parse(dr[1].ToString());
                    e1.s_end = DateTime.Parse(dr[2].ToString());
                    e1.attendence = dr[3].ToString();



                }
            }
            con.Close();
            return e1;


        }
    }
}
