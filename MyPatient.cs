﻿using System;                                                                                                                                                   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using System.Diagnostics;
using System.Data;

namespace Dclinic__system
{
    internal class MyPatient
    {
        public void AddPatient(String Query)
        {
            ConnectionString MyConnection = new ConnectionString();
            Debug.WriteLine("\n it's working");
            SqlConnection Con = MyConnection.GetCon();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Con;
            Con.Open(); 
            Cmd.CommandText = Query;
            Cmd.ExecuteNonQuery();
            Con.Close();

        }
        public void UpdatePatient(String Query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection Con = MyConnection.GetCon();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Con;
            Con.Open();
            Cmd.CommandText = Query;
            Cmd.ExecuteNonQuery();
            Con.Close();

        }

        public void DeletePatient(string Query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection Con = MyConnection.GetCon();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Con;
            Con.Open();
            Cmd.CommandText = Query;
            Cmd.ExecuteNonQuery();
            Con.Close();


        }
        public DataSet ShowPatient ( String Query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection Con = MyConnection.GetCon();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = Query;
            SqlDataAdapter sda = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;

        }


    }

}
