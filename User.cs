﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dclinic__system
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }
        void Populate()
        {

            MyPatient Pat = new MyPatient();
            String Query = " select * from UserTbl";
            DataSet ds = Pat.ShowPatient(Query);
            UserDGV.DataSource = ds.Tables[0];
        }

        private void User_Load(object sender, EventArgs e)
        {
            Populate();
        }
        void clear()
        {
            UNameTb.Text = "";
            UpswTb.Text = "";
            PhoneTb.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Input Validation
            if (string.IsNullOrWhiteSpace(UNameTb.Text))
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            if (string.IsNullOrWhiteSpace(UpswTb.Text) || UpswTb.Text.Length < 6)
            {
                MessageBox.Show("Please enter a valid password. Password must be at least 6 characters long.");
                return;
            }

            if (string.IsNullOrWhiteSpace(PhoneTb.Text) || !PhoneTb.Text.All(char.IsDigit) || PhoneTb.Text.Length != 10)
            {
                MessageBox.Show("Please enter a valid 10-digit phone number.");
                return;
            }

            // SQL Query
            String Query = "insert into UserTbl values  ( '" + UNameTb.Text + "','" + UpswTb.Text + "','" + PhoneTb.Text + "')";

            try
            {
                Users users = new Users();
                users.AddPatient(Query);
                MessageBox.Show("User Successfully Added");
                Populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

            clear();
        }

        int Key = 0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UNameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            UpswTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            PhoneTb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();

           if (UNameTb.Text == "")

           {
               Key = 0;
           }
            else
            {
               Key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[1].Value.ToString());

            }
        }

        
        

        private void button2_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (Key == 0)
            {
                MessageBox.Show("Select The User");
            }
            else
            {
                try
                {
                    String Query = " Delete  from UserTbl Where UId=" + Key + "";
                    Pat.DeletePatient(Query);
                    MessageBox.Show("User Deleted SUCCESSFULLY");
                    Populate();
                    Key = 0;
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Patient pat = new Patient();
            pat.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Doctors doc = new Doctors();
            doc.Show();
            this.Hide();
        }

        private void PhoneTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("error,phone number can,t contain letters");
            }

        }

        private void BindGrid()
        {
            SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from UserTbl ", Con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            UserDGV.DataSource = dt;
        }

        private void UserDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Key = Convert.ToInt32(UserDGV.Rows[e.RowIndex].Cells["UId"].FormattedValue.ToString());
            //if (MessageBox.Show("Are you sure to delete?", "Delete record", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    int id = Convert.ToInt32(UserDGV.Rows[e.RowIndex].Cells["UId"].FormattedValue.ToString());
            //    SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
            //    Con.Open();
            //    SqlCommand cmd = new SqlCommand("Delete  UserTbl where UId='" + id + "' ", Con);
            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("deleted Successfully");
            //    BindGrid();
            //    Con.Close();
            //}
        }
    }
}
