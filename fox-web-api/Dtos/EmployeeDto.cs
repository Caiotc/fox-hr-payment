using fox_web_api.Models;

namespace fox_web_api.Dtos
{
    public class EmployeeDto 
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public DateTime AdmissionDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; }

        public int DepartmentId { get; set; }
        public string Email  { get; set; }
        public string Position { get; set; }

        public decimal Income { get; set; }

        public string UserPhoto { get; set; }

    }
}
