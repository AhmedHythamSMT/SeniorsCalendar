using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeniorsCalender
{
    public partial class Form1 : Form
    {

        int month, year;
        private string loggedInUsername;
        public static int static_month, static_year;

        public Form1()
        {
            InitializeComponent();
            loggedInUsername = "Guest"; // Default username
            DisplayUsername();
            }
        private void DisplayUsername()
        {
            lblUsername.Text = loggedInUsername;

        }

        public Form1(string username)
        {
            InitializeComponent();
            loggedInUsername = username; // Use the provided username
            DisplayUsername();
        }
       


        private void Form1_Load(object sender, EventArgs e)
        {
            daysDisplay();
        }
        private void daysDisplay()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            static_month = month;
            static_year = year;

            

            DateTime startofMonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);

            int dayofWeek = Convert.ToInt32(startofMonth.DayOfWeek.ToString("d")) +2;

            for (int i = 1; i < dayofWeek; i++)
            {
                UserControl1Blank ucblank = new UserControl1Blank();
                daycontainer.Controls.Add(ucblank);
            }
            for (int i = 1; i <= days; i++)
            {
                UserControldays ucdays = new UserControldays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }

        }

        

        private void btnToDo_Click(object sender, EventArgs e)
        {
            ToDoForm todoForm = new ToDoForm(); // Pass this instance
            todoForm.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm(this); // Pass this instance
            login.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm(this); // Pass this instance
            register.Show();
        }


        private void PopulateUserControlDays()
        {
            // Clear the container where UserControlDays are hosted
            daycontainer.Controls.Clear();

            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            // Calculate month and year display
            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            static_month = month;
            static_year = year;

            DateTime startOfMonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);
            int dayOfWeek = Convert.ToInt32(startOfMonth.DayOfWeek.ToString("d")) + 2;

            // Add blank days for the first row
            for (int i = 1; i < dayOfWeek; i++)
            {
                UserControl1Blank ucBlank = new UserControl1Blank();
                daycontainer.Controls.Add(ucBlank);
            }

            // Add user control days
            for (int i = 1; i <= days; i++)
            {
                UserControldays ucDays = new UserControldays();
                ucDays.days(i);
                daycontainer.Controls.Add(ucDays);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    break;
                case 1:
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar");
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar");
                    break;
            }
            this.Controls.Clear();
            InitializeComponent();
            PopulateUserControlDays();
            this.Refresh();
        }
        private bool isLoggedIn = false;

        public void UpdateLoginStatus(string username)
        {
            isLoggedIn = true; // Set a flag for logged-in status
            loggedInUsername = username; // Update the displayed username
            DisplayUsername();

            // Hide the login and register buttons
            btnLogin.Visible = false;
            btnRegister.Visible = false;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            daycontainer.Controls.Clear();

            // Decrement the month
            month--;
            static_month = month;
            static_year = year;
            // Adjust the year if the month goes below January
            if (month < 1)
            {
                month = 12;
                year--;
            }
            
            // Update the label
            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            // Recalculate the days
            DateTime startofMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);

            int dayofWeek = Convert.ToInt32(startofMonth.DayOfWeek.ToString("d")) + 2;

            // Add blank user controls for padding
            for (int i = 1; i < dayofWeek; i++)
            {
                UserControl1Blank ucblank = new UserControl1Blank();
                daycontainer.Controls.Add(ucblank);
            }

            // Add day user controls
            for (int i = 1; i <= days; i++)
            {
                UserControldays ucdays = new UserControldays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }
        }


        private void bntNext_Click(object sender, EventArgs e)
        {
            daycontainer.Controls.Clear();

            // Increment the month
            month++;
            static_month = month;
            static_year = year;
            // Adjust the year if the month exceeds December
            if (month > 12)
            {
                month = 1;
                year++;
            }
            
            // Update the label
            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            // Recalculate the days
            DateTime startofMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);

            int dayofWeek = Convert.ToInt32(startofMonth.DayOfWeek.ToString("d")) + 2;

            // Add blank user controls for padding
            for (int i = 1; i < dayofWeek; i++)
            {
                UserControl1Blank ucblank = new UserControl1Blank();
                daycontainer.Controls.Add(ucblank);
            }

            // Add day user controls
            for (int i = 1; i <= days; i++)
            {
                UserControldays ucdays = new UserControldays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }
        }

    }
}