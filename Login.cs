using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dclinic__system
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        
        ConnectionString MyConnection = new ConnectionString();

        private void button1_Click(object sender, EventArgs e)
        {
            // Input Validation
            if (string.IsNullOrWhiteSpace(UnameTb.Text))
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            if (string.IsNullOrWhiteSpace(UpassTb.Text))
            {
                MessageBox.Show("Please enter a password.");
                return;
            }

            SqlConnection Con = MyConnection.GetCon();
            Con.Open();

            // Database Query
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM UserTbl WHERE Uname='" + UnameTb.Text + "' AND Upass='" + UpassTb.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                // Valid login
                Appointment App = new Appointment();
                App.Show();
                this.Hide();
            }
            else
            {
                // Invalid login
                MessageBox.Show("Wrong username or password.");
                UnameTb.Text = "";
                UpassTb.Text = "";
            }

            Con.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            AdminLogin log = new AdminLogin();
            log.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
