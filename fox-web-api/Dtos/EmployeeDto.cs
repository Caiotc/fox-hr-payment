using fox_web_api.Models;

namespace fox_web_api.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Address { get; set; }
        public string Adjutancy { get; set; }
        public DateTime AdmissionDate { get; set; }

        public int UserId { get; set; }


    }
}
