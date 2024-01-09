using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public class StudentDataResponseModel
    {
        public PageSettingModel PageSetting { get; set; }
        public List<StudentDataModel> Students { get; set; }
    }

    public class StudentListResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<StudentDataModel> ListStudentDataModel { get; set; }
    }

    public class StudentResponseModle
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public StudentDataModel StudentDataModel { get; set; }
    }

    public class PageSettingModel
    {
        public PageSettingModel()
        {       
        }
        public PageSettingModel(int pageNo, int pageSize, int pageCount, string pageUrl)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            PageCount = pageCount;
            PageUrl = pageUrl;
        }
        public int PageNo { get; set;}
        public int PageSize { get; set;}
        public int PageCount { get; set;}
        public string PageUrl { get; set; }
    }

    public class MessageModel
    {
        public MessageModel(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
