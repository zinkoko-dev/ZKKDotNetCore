namespace ZKKDotNetCore.RestAPI.Models
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
    }
}
