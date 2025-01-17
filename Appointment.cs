﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Dclinic__system
{
    public partial class Appointment : Form
    {
        public Appointment()
        {
            InitializeComponent();
        }
        ConnectionString MyCon = new ConnectionString();
        private void fillPatient()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand Cmd = new SqlCommand("select PatName from PatientTbl", Con);
            SqlDataReader rdr;
            rdr = Cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("patName", typeof(string));
            dt.Load(rdr);
            PatientCb.ValueMember = "PatName";
            PatientCb.DataSource = dt;
            Con.Close();
        }
       void clear()
        {
            PatientCb.Text = "";
            TimeCb.Text = "";

        }
        private void fillDoctor()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand Cmd = new SqlCommand("select DocName from DoctorTbl", Con);
            SqlDataReader rdr;
            rdr = Cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DocName", typeof(string));
            dt.Load(rdr);
            DocCb.ValueMember = "DocName";
            DocCb.DataSource = dt;
            Con.Close();
        }


        private void Appointment_Load(object sender, EventArgs e)
        {
            clear();
            fillPatient();
            fillDoctor();
            Populate();
            clear();
        }
        void Populate()
        {

            MyPatient Pat = new MyPatient();
            String Query = " select * from AppointmentTbl";
            DataSet ds = Pat.ShowPatient(Query);
            AppointmentDGV.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate Inputs
            if (PatientCb.SelectedValue == null || string.IsNullOrWhiteSpace(PatientCb.SelectedValue.ToString()))
            {
                MessageBox.Show("Please select a patient.");
                return;
            }

            if (DocCb.SelectedValue == null || string.IsNullOrWhiteSpace(DocCb.SelectedValue.ToString()))
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }

            if (TimeCb.SelectedItem == null || string.IsNullOrWhiteSpace(TimeCb.SelectedItem.ToString()))
            {
                MessageBox.Show("Please select a time for the appointment.");
                return;
            }

            if (Date.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("The appointment date cannot be in the past.");
                return;
            }

            SqlConnection con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");

            // Check for existing appointments
            string chek_query = "SELECT * FROM AppointmentTbl WHERE ApTime='" + TimeCb.SelectedItem.ToString() + "' AND ApDate='" + Date.Value.Date + "'";
            DataTable dt = new DataTable();
            SqlDataAdapter adat = new SqlDataAdapter(chek_query, con);
            adat.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("An appointment already exists for the selected time and doctor.");
            }
            else
            {
                string Query = "INSERT INTO AppointmentTbl VALUES( '" + PatientCb.SelectedValue.ToString() + "' ,'" + DocCb.SelectedValue.ToString() + "' ,'" + Date.Value.Date + "','" + TimeCb.SelectedItem.ToString() + "')";
                MyPatient Pat = new MyPatient();
                try
                {
                    Pat.AddPatient(Query);
                    MessageBox.Show("Appointment successfully recorded.");
                    Populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }








        private void button2_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (Key == 0)
            {
                MessageBox.Show("Select The Appointment to cancel");
            }
            else
            {
                try
                {
                    String Query = " Delete  from AppointmentTbl Where ApId=" + Key + "";
                    Pat.DeletePatient(Query);
                    MessageBox.Show("Appointment Deleted SUCCESSFULLY");
                    Populate();
                    Key = 0;
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }
        int Key = 0;

        

        private void label2_Click(object sender, EventArgs e)
        {
            Patient pat = new Patient();
            pat.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Treatment treat = new Treatment();
            treat.Show();
            this.Hide();
        }

      

        private void label6_Click(object sender, EventArgs e)
        {
            //Dashboard dash = new Dashboard();
           // dash.show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Doctors doc = new Doctors();
            doc.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            prescription prec = new prescription();
            prec.Show();
            this.Hide();
        }
        private void BindGrid()
        {
            SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from AppointmentTbl ", Con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
           AppointmentDGV.DataSource = dt;
        }

        private void AppointmentDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            Key = Convert.ToInt32(AppointmentDGV.Rows[e.RowIndex].Cells["ApId"].FormattedValue.ToString());
            //if (MessageBox.Show("Are you sure to delete?", "Delete record", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    int id = Convert.ToInt32(AppointmentDGV.Rows[e.RowIndex].Cells["ApId"].FormattedValue.ToString());
            //    SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
            //    Con.Open();
            //    SqlCommand cmd = new SqlCommand("Delete  AppointmentTbl where ApId='" + id + "' ", Con);
            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("deleted Successfully");
            //    BindGrid();
            //    Con.Close();
            //}
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Patient pat = new Patient();
            pat.Show();
            this.Hide();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            Doctors pat = new Doctors();
            pat.Show();
            this.Hide();
        }

        private void label6_Click_1(object sender, EventArgs e)
        {
            prescription pat = new prescription();
            pat.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            User pat = new User();
            pat.Show();
            this.Hide();
        }

        private void AppointmentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
