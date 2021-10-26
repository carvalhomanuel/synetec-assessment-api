namespace SynetecAssessmentApi.Application.RequestModels
{
    public class EmployeeRequestModel
    {
        public string Fullname { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
