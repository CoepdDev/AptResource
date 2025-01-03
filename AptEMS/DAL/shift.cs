using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;

namespace AptEMS.DAL
{
    public class shift
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.
              ConnectionStrings["cs"].ToString());
        public int Addshift(Models.shift e1)
        {

            con.Open();
            // Check if the ID already exists
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.shift WHERE sno = @sno", con))
            {
                checkCmd.Parameters.AddWithValue("@sno", e1.sno);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // ID already exists, return a value to indicate failure or handle as needed
                    con.Close();
                    return -1; // Indicating duplicate ID, or you can throw an exception or handle differently
                }
            }



            SqlCommand cmd = new SqlCommand("pro_addshift", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sno", e1.sno);
            cmd.Parameters.AddWithValue("@shiftname", e1.shiftname);
            cmd.Parameters.AddWithValue("@created", e1.created);
            int i = cmd.ExecuteNonQuery();
            con.Close();


            return i;
        }
        public int Deleteshift(Models.shift e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("pro_deleteshift", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sno", e1.sno);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }
        public int Updateshift(Models.shift e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("pro_updateshift", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sno", e1.sno);
            cmd.Parameters.AddWithValue("@shiftname", e1.shiftname);
            cmd.Parameters.AddWithValue("@created", e1.created);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }
        public List<Models.shift> Getshift()
        {
            List<Models.shift> li = new List<Models.shift>();


            con.Open();
            SqlCommand cmd = new SqlCommand("pro_getshift", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Models.shift e1 = new Models.shift();
                    e1.sno = int.Parse(dr[0].ToString());
                    e1.shiftname = dr[1].ToString();
                    e1.created = DateTime.Parse(dr[2].ToString());
                    li.Add(e1);
                }

            }
            con.Close();
            return li;


        }
        public Models.shift Searchshift(Models.shift e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("pro_searchshift", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sno", e1.sno);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    e1.shiftname = dr[0].ToString();
                    e1.created = DateTime.Parse(dr[1].ToString());
                }
            }
            con.Close();
            return e1;

        }
    }
}