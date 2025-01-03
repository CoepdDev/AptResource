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
    public class Hike
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.
         ConnectionStrings["cs"].ToString());

        public int AddHike(Models.Hike e1)
        {

            con.Open();
            using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Hike WHERE Empid = @Empid", con))
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
            SqlCommand cmd = new SqlCommand("sp_InsertHike", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@FirstName", e1.FirstName);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@Position", e1.Position);
            cmd.Parameters.AddWithValue("@Dateofstart", e1.Dateofstart);
            cmd.Parameters.AddWithValue("@Basesalary", e1.Basesalary);
            cmd.Parameters.AddWithValue("@Hike", e1.Hikee);
            cmd.Parameters.AddWithValue("@Reporter", e1.Reporter);
            cmd.Parameters.AddWithValue("@Approver", e1.Approver);
            cmd.Parameters.AddWithValue("@Department", e1.Department);

            int i = cmd.ExecuteNonQuery();
            con.Close();


            return i;
        }

        public int DeleteHike(Models.Hike e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_DeleteHike", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Empid", e1.Empid);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

        public int UpdateHike(Models.Hike e1)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateHike", con);
            cmd.CommandType = CommandType.StoredProcedure;





            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            cmd.Parameters.AddWithValue("@FirstName", e1.FirstName);
            cmd.Parameters.AddWithValue("@Designation", e1.Designation);
            cmd.Parameters.AddWithValue("@Position", e1.Position);
            cmd.Parameters.AddWithValue("@Dateofstart", e1.Dateofstart);
            cmd.Parameters.AddWithValue("@Basesalary", e1.Basesalary);
            cmd.Parameters.AddWithValue("@Hike", e1.Hikee);
            cmd.Parameters.AddWithValue("@Reporter", e1.Reporter);
            cmd.Parameters.AddWithValue("@Approver", e1.Approver);
            cmd.Parameters.AddWithValue("@Department", e1.Department);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;


        }

       

        public List<Models.Hike> GetHike()
        {
            List<Models.Hike> li = new List<Models.Hike>();


            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetAllHikes", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Models.Hike e1 = new Models.Hike();


                    e1.Empid = int.Parse(dr[0].ToString());
                    e1.FirstName = dr[1].ToString();
                    e1.Designation = dr[2].ToString();
                    e1.Position = dr[3].ToString();
                    e1.Dateofstart = DateTime.Parse(dr[4].ToString());
                    e1.Basesalary = int.Parse(dr[5].ToString());
                    e1.Hikee = int.Parse(dr[6].ToString());
                    e1.Reporter = dr[7].ToString();
                    e1.Approver = dr[8].ToString();
                    e1.Department = dr[9].ToString();


                    li.Add(e1);
                }

            }
            con.Close();
            return li;


        }
        public Models.Hike SearchHike(Models.Hike e1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SearchHike", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid", e1.Empid);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    e1.FirstName = dr[0].ToString();
                    e1.Designation = dr[1].ToString();
                    e1.Position = dr[2].ToString();
                    e1.Dateofstart = DateTime.Parse(dr[3].ToString());
                    e1.Basesalary = int.Parse(dr[4].ToString());
                    e1.Hikee = int.Parse(dr[5].ToString());
                    e1.Reporter = dr[6].ToString();
                    e1.Approver = dr[7].ToString();
                    e1.Department = dr[8].ToString();

                }
            }
            con.Close();
            return e1;

        }


        public Models.Hike GetHikeDetailsByEmpId(Models.Hike e1)
        {
            //Models.Hike hikeDetails = new Models.Hike();

           
            
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetHikeDetailsByEmpId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Empid", e1.Empid);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                    e1.Empid = int.Parse(dr["Empid"].ToString());
                    e1.FirstName = dr["FirstName"].ToString();
                    e1.Designation = dr["Designation"].ToString();
                    e1.Position = dr["Position"].ToString();
                    e1.Dateofstart = DateTime.Parse(dr["Dateofstart"].ToString());
                    e1.Basesalary = float.Parse(dr["Basesalary"].ToString());
                    e1.Hikee = float.Parse(dr["Hike"].ToString());
                    e1.Reporter = dr["Reporter"].ToString();
                    e1.Approver = dr["Approver"].ToString();
                    e1.Department = dr["Department"].ToString();
                    }
                }
                con.Close();
            

            return  e1;
        }




    }
}