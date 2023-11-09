using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZKKDotNetCore.MvcApp.Models
{
    [Table("Tbl_Student")]
    public class StudentDataModel
    {
        [Key]
        public int Student_Id { get; set; }
        public string Student_Name { get; set; }
        public string Student_City { get; set; }
        public string Student_Gender { get; set; }

    }
}
