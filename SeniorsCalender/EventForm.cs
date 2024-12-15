using System;
using System.Windows.Forms;
using MySqlConnector;

namespace SeniorsCalender
{
    public partial class EventForm : Form
    {
        private UserControldays parentControl;
        private string eventDate;  // Store the date of the event
        private bool isEditing = false;  // To track if we are in edit mode

        String connString = "server=localhost;user id=root;database=db_calendar;sslmode=none;Pooling=false;";
        public EventForm(UserControldays control)
        {
            InitializeComponent();
            parentControl = control;
        }

        private void txDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void EventForm_Load(object sender, EventArgs e)
        {
            txDate.Text = $"{Form1.static_year}-{Form1.static_month}-{UserControldays.static_day}";
            
        }

        // Method to load event data from the database and populate fields
        public void LoadEventData()
        {
            eventDate = $"{Form1.static_year}-{Form1.static_month}-{UserControldays.static_day}";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM tbl_calendar WHERE date = @date";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@date", eventDate);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate fields with event data
                                txId.Text = reader["id"].ToString();
                                txName.Text = reader["name"].ToString();
                                txDesc.Text = reader["description"].ToString();
                                txDate.Text = reader["date"].ToString();

                                // Make fields read-only by default
                                SetFieldsEditable(false);  // Fields are read-only after loading event data
                                btnUpdate.Enabled = true;    // Enable the Edit button
                            }
                            else
                            {
                                // If no event exists, clear fields and notify
                                txId.Clear();
                                txName.Clear();
                                txDesc.Clear();
                                txDate.Text = $"{Form1.static_month}/{UserControldays.static_day}/{Form1.static_year}";

                                MessageBox.Show("No event found for the selected date.");
                                btnUpdate.Enabled = false; // Disable edit button if no event exists
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

        // Method to set fields as read-only or editable
        private void SetFieldsEditable(bool editable)
        {
            txName.ReadOnly = !editable;
            txDesc.ReadOnly = !editable;
            txDate.ReadOnly = !editable;
        }

        // Handle the Edit button click
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                // If we are in editing mode, save the event
                btnUpdate.Text = "Edit";
                SetFieldsEditable(false);  // Make fields read-only again
                // Save the updated event data
                SaveEventData();

            }
            else
            {
                // Switch to edit mode
                btnUpdate.Text = "Save";
                SetFieldsEditable(true);  // Make fields editable
                
            }

            isEditing = !isEditing;  // Toggle edit mode
        }

        private void SaveEventData()
        {
            // Ensure txId.Text is not empty before updating the event
            if (string.IsNullOrWhiteSpace(txId.Text))
            {
                MessageBox.Show("Event ID is missing. Cannot update event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Open a new connection to the database
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    // Prepare the SQL query for updating the event
                    string sql = "UPDATE tbl_calendar SET name = @name, description = @description, date = @date WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Set parameters to ensure values are correctly passed
                        cmd.Parameters.AddWithValue("@id", txId.Text);  // Event ID to update
                        cmd.Parameters.AddWithValue("@name", txName.Text);  // New event name
                        cmd.Parameters.AddWithValue("@description", txDesc.Text);  // New event description
                        cmd.Parameters.AddWithValue("@date", txDate.Text);  // New event date

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if the update was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Event Updated Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            parentControl.LoadEventData();  // Refresh the event display in UserControldays
                        }
                        else
                        {
                            MessageBox.Show("Event not found or failed to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the update
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Add new event logic
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txName.Text) || string.IsNullOrWhiteSpace(txDesc.Text))
            {
                MessageBox.Show("Event name and description cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string sql = "INSERT INTO tbl_calendar(name, description, date) VALUES (?, ?, ?)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", txName.Text);
                        cmd.Parameters.AddWithValue("@description", txDesc.Text);
                        cmd.Parameters.AddWithValue("@date", txDate.Text);

                        cmd.ExecuteNonQuery();
                    }

                    // Retrieve the last inserted ID
                    string getIdSql = "SELECT LAST_INSERT_ID()";
                    using (MySqlCommand cmd = new MySqlCommand(getIdSql, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            txId.Text = result.ToString();  // Display the new ID in the txID textbox
                            MessageBox.Show("Event Added Successfully. Event ID: " + txId.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Set the fields to read-only after adding
                            SetFieldsEditable(false);
                            btnUpdate.Enabled = true;  // Enable the Edit button
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
   
    private void btnDelete_Click(object sender, EventArgs e)
        {
            // Ensure the user has entered an ID
            if (string.IsNullOrWhiteSpace(txId.Text))
            {
                MessageBox.Show("Please enter the ID of the event to delete.", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Confirm deletion
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this event?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        conn.Open();

                        // SQL statement to delete the event by ID
                        string sql = "DELETE FROM tbl_calendar WHERE id = @id";

                        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                        {
                            // Add the ID parameter
                            cmd.Parameters.AddWithValue("@id", txId.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            ClearFields();

                            SetFieldsEditable(true);

                            // Check if a row was deleted
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Event deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No event found with the provided ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public void ClearFields()
        {
            txId.Text = "";
            txName.Text = "";
            txDesc.Text = "";
        }



    }
}
