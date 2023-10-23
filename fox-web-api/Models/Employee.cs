using System.ComponentModel.DataAnnotations.Schema;

namespace fox_web_api.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Address { get; set; }
        public string Adjutancy { get; set; }
        public double Income { get; set; }
        public DateTime AdmissionDate { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
