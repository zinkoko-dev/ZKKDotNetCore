namespace ZKKDotNetCore.MvcApp.Models
{
    public class StudentResponseModle
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public StudentDataModel StudentDataModel { get; set; }
    }

    public class StudentListResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<StudentDataModel> ListStudentDataModel { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageRowCount { get; set; }
    }
}
