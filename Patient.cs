using System;
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

namespace Dclinic__system
{
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
        }

       

           

    




       /* private void button1_Click(object sender, EventArgs e) 
        {
            /*
            String Query = "insert into PatientTbl values  ( '" + PatNameTb.Text + "','" + textBox3.Text + "','" + AddressTb.Text + "','" + DOBDate.Value.Date + "','" + GenderCb.SelectedItem.ToString() + "','" + AllergyTb.Text + "','" + CnicTb.Text + "'";
            MyPatient Pat = new MyPatient();
            try
            {
                Pat.AddPatient(Query);
                MessageBox.Show("Patient Suucessfully Added");
                Populate();
                Cleardata();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            

        }*/


        private void Cleardata()
        {
            PatNameTb.Text = "";
            textBox3.Text = "";
            AddressTb.Text = "";
            DOBDate.Text = "";
            AllergyTb.Text = "";
            GenderCb.SelectedItem = "";
            CnicTb.Text = "";
          
        }
        void Populate()
        {

            MyPatient Pat = new MyPatient();
            String Query = " select * from PatientTbl";
            DataSet ds = Pat.ShowPatient(Query);
            PatientDGV.DataSource = ds.Tables[0];
        }


        private void Patient_Load(object sender, EventArgs e)
        {
            Populate();
        }
        int Key = 0;


        private void button2_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (Key == 0)
            {
                MessageBox.Show("Select The Patient");
            }
            else
            {
                try
                {
                    String Query = " Delete  from PatientTbl Where PatId=" + Key + "";
                    Pat.DeletePatient(Query);
                    MessageBox.Show("Patient Deleted SUCCESSFULLY");
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

        private void label8_Click(object sender, EventArgs e)
        {
            Appointment App = new Appointment();
            App.Show();
            this.Hide();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Treatment treat = new Treatment();
            treat.Show();
            this.Hide();
        }



        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("error,phone number can,t contain letters");
            }


        }

        private void label4_Click(object sender, EventArgs e)
        {
            Doctors doc = new Doctors();
            doc.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            prescription prec = new prescription();
            prec.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            DashBoard dsh = new DashBoard();
            dsh.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            User usr = new User();
            usr.Show();
            this.Hide();
        }

        private void DOBDate_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Today < DOBDate.Value)
            {
                MessageBox.Show("invalid date ");
                DOBDate.Value = DateTime.Today;

            }
        }
        private void BindGrid()
        {
            SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from PatientTbl ", Con);
            
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            Console.WriteLine("\n gridview data : " + dt);
            PatientDGV.DataSource = dt;
        }

        private void PatientDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Key = Convert.ToInt32(PatientDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
            //if (MessageBox.Show("Are you sure to delete?", "Delete record", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    int id = Convert.ToInt32(PatientDGV.Rows[e.RowIndex].Cells["PatId"].FormattedValue.ToString());
            //    SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
            //    Con.Open();
            //    SqlCommand cmd = new SqlCommand("Delete  PatientTbl where PatId='" + id + "' ", Con);
            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("deleted Successfully");
            //    BindGrid();
            //    Con.Close();
            //}
        }
        private bool patientValidation()
        {
            // Patient Name Validation
            if (string.IsNullOrWhiteSpace(PatNameTb.Text))
            {
                MessageBox.Show("Please enter the patient's name.");
                return false;
            }

            // Phone Number Validation
            if (string.IsNullOrWhiteSpace(textBox3.Text) || !textBox3.Text.All(char.IsDigit) || textBox3.Text.Length != 10)
            {
                MessageBox.Show("Please enter a valid 10-digit phone number.");
                return false;
            }

            // Address Validation
            if (string.IsNullOrWhiteSpace(AddressTb.Text))
            {
                MessageBox.Show("Please enter the patient's address.");
                return false;
            }

            // Date of Birth Validation
            if (DOBDate.Value.Date >= DateTime.Now.Date)
            {
                MessageBox.Show("Please select a valid date of birth.");
                return false;
            }

            // Gender Validation
            if (GenderCb.SelectedItem == null || string.IsNullOrWhiteSpace(GenderCb.SelectedItem.ToString()))
            {
                MessageBox.Show("Please select the patient's gender.");
                return false;
            }

            // Allergy Information Validation
            if (string.IsNullOrWhiteSpace(AllergyTb.Text))
            {
                MessageBox.Show("Please enter allergies information or 'None' if no allergies.");
                return false;
            }

            // CNIC Validation
            if (string.IsNullOrWhiteSpace(CnicTb.Text) || CnicTb.Text.Length != 13 || !CnicTb.Text.All(char.IsDigit))
            {
                MessageBox.Show("Please enter a valid 13-digit CNIC.");
                return false;
            }

            // All validations passed
            return true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(!patientValidation())
            {

                return;
            }

            SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
            Con.Open();
            SqlCommand cmd = new SqlCommand("insert into PatientTbl values (@PatName,@PatPhone,@PatAddress,@PatDOB,@PatGender,@PatAllergies,@Cnic)", Con);
            cmd.Parameters.AddWithValue("@PatName", PatNameTb.Text);
            cmd.Parameters.AddWithValue("@PatPhone", textBox3.Text);
            cmd.Parameters.AddWithValue("@PatAddress", AddressTb.Text);
            cmd.Parameters.AddWithValue("@PatDOB", DOBDate.Value.Date);
            cmd.Parameters.AddWithValue("@PatGender", GenderCb.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@PatAllergies", AllergyTb.Text);
            cmd.Parameters.AddWithValue("@Cnic", CnicTb.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Added");
            BindGrid();
            Cleardata();
            Con.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Patient");
                return;
            }

            if (!patientValidation())
            {
                return;
            }

            try
            {
                SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
                Con.Open();

                // SQL command with parameters
                SqlCommand cmd = new SqlCommand("update PatientTbl set PatName=@PatName, PatPhone=@PatPhone, PatAddress=@PatAddress, PatDOB=@PatDOB, PatGender=@PatGender, PatAllergies=@PatAllergies, Cnic=@Cnic where PatId=@PatId", Con);

                // Add parameter values
                cmd.Parameters.AddWithValue("@PatName", PatNameTb.Text);
                cmd.Parameters.AddWithValue("@PatPhone", textBox3.Text);
                cmd.Parameters.AddWithValue("@PatAddress", AddressTb.Text);
                cmd.Parameters.AddWithValue("@PatDOB", DOBDate.Value.Date);
                cmd.Parameters.AddWithValue("@PatGender", GenderCb.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@PatAllergies", AllergyTb.Text);
                cmd.Parameters.AddWithValue("@Cnic", CnicTb.Text);
                cmd.Parameters.AddWithValue("@PatId", Key);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully Updated");
                BindGrid();
                Cleardata();
                Key = 0;
                Con.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }



        /*  int id = 0;
private void button3_Click(object sender, EventArgs e)
{

SqlConnection Con = new SqlConnection("Data Source=ZAIN-KHAN\\SQLEXPYASIN;Initial Catalog=Al_shifa;Integrated Security=True;Persist Security Info=False;Pooling=False;");
Con.Open();
SqlCommand cmd = new SqlCommand("update  PatientTbl set PatName=@PatName,PatPhone=@PatPhone,PatAddress=@PatAddress,PatDOB=@PatDOB,PAtGender=@PatGender, PAtAllergies=@PAtAllergies ,Cnic=@Cnic where PatId="+ id +"", Con);

cmd.ExecuteNonQuery();

MessageBox.Show("successfully Updated");
Cleardata();
Con.Close();
}*/
    }


}
