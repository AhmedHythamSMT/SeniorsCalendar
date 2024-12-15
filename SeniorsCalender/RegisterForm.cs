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
    public partial class RegisterForm : Form
    {
        String connString = "server=localhost;user id=root;database=db_calendar;sslmode=none;Pooling=false;";
        private Form1 mainForm;

        public RegisterForm(Form1 parentForm)
        {
            InitializeComponent();
            mainForm = parentForm; // Assign the Form1 instance
        }

        public void register()
        {

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string sql = "INSERT INTO tbl_users(username, email, password) VALUES (?, ?, ?)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txUser.Text);
                        cmd.Parameters.AddWithValue("@email", txEmail.Text);
                        cmd.Parameters.AddWithValue("@password", txPass.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("You Registered Successfully.\nYou will be redirected to login page to login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    LoginForm login = new LoginForm(mainForm);
                    login.Show();
                }
            }

            catch (Exception ex)
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
            LoginForm login = new LoginForm(mainForm);
            login.Show();

            // Check if RegisterForm is already open, and close it
            foreach (Form form in Application.OpenForms)
            {
                if (form is RegisterForm)
                {
                    form.Close();
                    break;
                }
            }


        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            register();
        }
    }
}
