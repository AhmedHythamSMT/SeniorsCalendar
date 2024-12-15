using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace SeniorsCalender
{
    public partial class UserControldays : UserControl
    {
        public static string static_day;
        String connString = "server=localhost;user id=root;database=db_calendar;sslmode=none;Pooling=false;";

        public UserControldays()
        {
            InitializeComponent();
        }

        private void UserControldays_Load(object sender, EventArgs e)
        {
            // Load the event data when the user control is loaded
            LoadEventData();
        }

        public void days(int numday)
        {
            lbdays.Text = numday + "";
            LoadEventData(); // Make sure we load events when days are set
        }

        private void UserControldays_Click(object sender, EventArgs e)
        {
            static_day = lbdays.Text;
            timer1.Start();
            EventForm eventForm = new EventForm(this); // Pass this instance
            eventForm.LoadEventData(); // Call the method to load event data
            eventForm.Show();
        }



        // Load event data from database and display it
        public void LoadEventData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM tbl_calendar WHERE date = @date";
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        // Format the date in the proper format: YYYY-MM-DD
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@date", $"{Form1.static_year}-{Form1.static_month}-{lbdays.Text}");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Display event ID
                                if (lbEvID != null) lbEvID.Text = "ID: " + reader["id"].ToString();

                                // Display event name
                                lbEvName.Text = reader["name"].ToString();
                            }
                            else
                            {
                                // No event found for the selected date
                                if (lbEvID != null) lbEvID.Text = "";
                                lbEvName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadEventData(); // Refresh event data every tick
        }
    }
}
