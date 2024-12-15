using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace SeniorsCalender
{
    public partial class LoginForm : Form
    {

        String connString = "server=localhost;user id=root;database=db_calendar;sslmode=none;Pooling=false;";
        private Form1 mainForm;

        public LoginForm(Form1 parentForm)
        {
            InitializeComponent();
            mainForm = parentForm; // Assign the Form1 reference
        }



        public void login()
        {
            string sql = "SELECT * FROM tbl_users WHERE username = '" +txUser.Text+ "' AND password = '" +txPass.Text+ "'";

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandTimeout = 60;

            // Use parameterized query to prevent SQL Injection
            cmd.Parameters.AddWithValue("@username", txUser.Text.Trim());
            cmd.Parameters.AddWithValue("@password", txPass.Text.Trim());
            MySqlDataReader reader;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        string loggedInUser = reader["username"].ToString();
                        mainForm.UpdateLoginStatus(loggedInUser);
                        mainForm.Show();
                        MessageBox.Show("Login Successfully.\nYou will be redirected to Calendar.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Oops! Their is Something went wrong. Try Again");
                    }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }
    }
}
