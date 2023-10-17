using Dapper;
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
using ZKKDotNetCore.WindowsFormsApp.Models;

namespace ZKKDotNetCore.WindowsFormsApp
{
    public partial class StudentDapper : Form
    {
        private readonly SqlConnection _connection;
        public StudentDapper()
        {
            InitializeComponent();
            AppConfigService appConfigService  = new AppConfigService();
            _connection = new SqlConnection(appConfigService.GetDbConnection());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string query = $@"insert into [dbo].[Tbl_Student]
                            ([Student_Name],[Student_City],[Student_Gender])
                            values
                            (@Student_Name, @Student_City, @Student_Gender);";

            StudentDataModel studentDataModel = new StudentDataModel()
            {
                Student_Name = txtName.Text,
                Student_City = txtCity.Text,
                Student_Gender = txtGender.Text,
            };

            using (IDbConnection db = new SqlConnection(_connection.ConnectionString))
            {
                var result = db.Execute(query, studentDataModel);
                string message = result > 0 ? "Save Successful" : "Error!!";
                MessageBox.Show(message, "Alert", MessageBoxButtons.OK,
                                result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            };

            txtName.Clear();
            txtCity.Clear();
            txtGender.Clear();
            txtName.Focus();
        }
    }
}
