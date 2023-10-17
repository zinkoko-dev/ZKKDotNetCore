using System;
using System.Windows.Forms;
using ZKKDotNetCore.WindowsFormsApp.Models;

namespace ZKKDotNetCore.WindowsFormsApp
{
    public partial class StudentEF : Form
    {
        private readonly AppDbContext _context;

        public StudentEF()
        {
            InitializeComponent();
            AppConfigService appConfigService = new AppConfigService();
            _context = new AppDbContext(appConfigService.GetDbConnection());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            StudentDataModel studentDataModel = new StudentDataModel()
            {
                Student_Name = txtName.Text,
                Student_City = txtCity.Text,
                Student_Gender = txtGender.Text
           };

            _context.Students.Add(studentDataModel);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Save Successful" : "Error!!";
            MessageBox.Show(message, "Alert", MessageBoxButtons.OK,
                            result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            txtName.Clear();
            txtCity.Clear();
            txtGender.Clear();
            txtName.Focus();
        }
    }
}