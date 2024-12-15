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
    public partial class ToDoForm : Form
    {
        String connString = "server=localhost;user id=root;database=db_calendar;sslmode=none;Pooling=false;";

        public ToDoForm()
        {
            InitializeComponent();
        }

        private void ToDoForm_Load(object sender, EventArgs e)
        {
            FillDGV("");
        }

        public void FillDGV(string searchValue)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT * FROM tbl_calendar WHERE CONCAT(id,name,description,date) LIKE '%" + searchValue + "%' ";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();

                    adapter.Fill(table);
                    dataGridView1.RowTemplate.Height = 60;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.DataSource= table ;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill ;

                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txDesc.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txDate.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string sql = "INSERT INTO tbl_calendar(id, name, description, date) VALUES (@id, @name, @description, @date)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", MySqlDbType.VarChar).Value = txID.Text;
                        cmd.Parameters.AddWithValue("@name", MySqlDbType.VarChar).Value = txName.Text;
                        cmd.Parameters.AddWithValue("@description", MySqlDbType.VarChar).Value = txDesc.Text;
                        cmd.Parameters.AddWithValue("@date", MySqlDbType.VarChar).Value = txDate.Text;

                        ExecuteMyQuery(cmd,"Event Add Successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ExecuteMyQuery(MySqlCommand mcmd, string msg)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            if (mcmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show(msg);
            }
            else
            {
                MessageBox.Show("Query Not Executed");
            }
            conn.Close();
            FillDGV("");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string sql = "UPDATE tbl_calendar SET name = @name, description = @description, date = @date WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", MySqlDbType.VarChar).Value = txID.Text;
                        cmd.Parameters.AddWithValue("@name", MySqlDbType.VarChar).Value = txName.Text;
                        cmd.Parameters.AddWithValue("@description", MySqlDbType.VarChar).Value = txDesc.Text;
                        cmd.Parameters.AddWithValue("@date", MySqlDbType.VarChar).Value = txDate.Text;

                        ExecuteMyQuery(cmd, "Event Updated Successfully");
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
            // Confirm deletion
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this event?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        conn.Open();
                        string sql = "DELETE FROM tbl_calendar WHERE id = @id";
                        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", MySqlDbType.VarChar).Value = txID.Text;

                            ExecuteMyQuery(cmd, "Event Deleted Successfully");
                            ClearFields();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void txSearch_TextChanged(object sender, EventArgs e)
        {
            FillDGV(txSearch.Text);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        public void ClearFields()
        {
            txID.Text = "";
            txName.Text = "";
            txDesc.Text = "";
            txDate.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
