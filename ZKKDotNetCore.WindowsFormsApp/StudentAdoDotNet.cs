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
using System.Xml.Linq;

namespace ZKKDotNetCore.WindowsFormsApp
{
    public partial class StudentAdoDotNet : Form
    {
        private readonly SqlConnection _connection;
        public StudentAdoDotNet()
        {
            InitializeComponent();
            AppConfigService appConfigService = new AppConfigService();
            _connection = new SqlConnection(appConfigService.GetDbConnection());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string query = $@"insert into [dbo].Tbl_Student
                            ([Student_Name],[Student_City],[Student_Gender])
                            values
                            (@Student_Name, @Student_City, @Student_Gender);";

            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Student_Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Student_City", txtCity.Text);
            cmd.Parameters.AddWithValue("@Student_Gender", txtGender.Text);

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Saving Successful !!" : "Error While Saving !!";

            MessageBox.Show(message, "Alert", MessageBoxButtons.OK,
                                result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            txtName.Clear();
            txtCity.Clear();
            txtGender.Clear();
            txtName.Focus();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            string query = "select * from tbl_student";

            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
        }
    }
}