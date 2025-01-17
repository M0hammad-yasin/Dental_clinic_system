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
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace Dclinic__system
{
    public partial class Doctors : Form
    {
        public Doctors()
        {
            InitializeComponent();
        }


        SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
        int key = 0;
        private void button1_Click(object sender, EventArgs e){
    // Check if any required field is empty
    if (string.IsNullOrWhiteSpace(DocNameTb.Text))
    {
        MessageBox.Show("Doctor Name is required.");
        return;
    }

    if (GenderCb.SelectedItem == null)
    {
        MessageBox.Show("Please select a gender.");
        return;
    }

    if (string.IsNullOrWhiteSpace(DocPhone.Text) || !System.Text.RegularExpressions.Regex.IsMatch(DocPhone.Text, @"^\d{10}$"))
    {
        MessageBox.Show("Please enter a valid 10-digit phone number.");
        return;
    }

    if (string.IsNullOrWhiteSpace(DocAddressTb.Text))
    {
        MessageBox.Show("Address is required.");
        return;
    }

    if (string.IsNullOrWhiteSpace(emailTb.Text) || !System.Text.RegularExpressions.Regex.IsMatch(emailTb.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
    {
        MessageBox.Show("Please enter a valid email address.");
        return;
    }

    try
    {
        Con.Open();

        SqlCommand cmd = new SqlCommand("insert into DoctorTbl values (@DocName,@DocGEN,@DocPHONE,@DocAdd, @Docemail)", Con);
        cmd.Parameters.AddWithValue("@DocName", DocNameTb.Text);
        cmd.Parameters.AddWithValue("@DocGEN", GenderCb.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@DocPHONE", DocPhone.Text);
        cmd.Parameters.AddWithValue("@DocAdd", DocAddressTb.Text);
        cmd.Parameters.AddWithValue("@Docemail", emailTb.Text);

        cmd.ExecuteNonQuery();
        MessageBox.Show("Successfully Added");

        // Clear and reload records
        clear();
        loadrecord();
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error: {ex.Message}");
    }
    finally
    {
        Con.Close();
    }
}

        SqlConnection con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");

        void loadrecord()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("exec showrecord_sp ", con);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            DDGV.DataSource = dt;
            con.Close();

        }

        private void clear()
        {
            DocAddressTb.Text = "";
          
            DocPhone.Text = "";
            GenderCb.Text = "";
            DocNameTb.Text = "";
            emailTb.Text = "";

        }
  
        private void label8_Click(object sender, EventArgs e)
        {
            Appointment App = new Appointment();
            App.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Patient Pat = new Patient();
            Pat.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Treatment treat = new Treatment();
            treat.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
           User user = new User();
            user.Show();
            this.Hide();
        }
       

        private void DocPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("error! phone contain letters");
            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from DoctorTbl ", Con);
            cmd.Parameters.AddWithValue("@DocName", DocNameTb.Text);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            DDGV.DataSource = dt;
            BindGrid();
            Con.Close();

        }
        private void BindGrid()
        {
            SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from DoctorTbl ", Con);
           
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            DDGV.DataSource = dt;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            prescription prec = new prescription();
            prec.Show();
            this.Hide();
        }

       
    
    



        

    private void emailTb_Leave(object sender, EventArgs e)
        {
            /* string pattern = " ^{ [0-9a-zA-Z]([-\\.\\w]*@([0-9a-zA-Z][-\\w]*[0-9a-zA-z]\\.)+[a-zA-Z]{2,9})$"; */
            string pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" +
                             "@" +
                             @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

           
            if ( Regex.IsMatch(emailTb.Text,pattern))
            {
                errorProvider1.Clear();
            }
        else
            {
                errorProvider1.SetError(this.emailTb, "plz provide valid mail");
              
                return;
            }

        }

        private void DDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            key = Convert.ToInt32(DDGV.SelectedRows[0].Cells[0].Value.ToString());
            //if (MessageBox.Show("Are you sure to delete?", "Delete record", MessageBoxButtons.YesNo) == DialogResult.Yes) 
            //{
            //    int id = Convert.ToInt32(DDGV.Rows[e.RowIndex].Cells["DocId"].FormattedValue.ToString());
            //    SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
            //    Con.Open();
            //    SqlCommand cmd = new SqlCommand("Delete  DoctorTbl where DocId='" + id + "' ", Con);
            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("deleted Successfully");
            //    BindGrid();
            //    Con.Close();
            //}
        }

        private void Doctors_Load(object sender, EventArgs e)
        {
            loadrecord();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login user = new Login();
            user.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Doctor to Update");
            }
            else
            {
                try
                {
                    SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete  DoctorTbl where DocId='" + key + "' ", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("deleted Successfully");
                    BindGrid();
                    Con.Close();
                    loadrecord();
                    clear();
                    key = 0;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
           
        }
    }
}
