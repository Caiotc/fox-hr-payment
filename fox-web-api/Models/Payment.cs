using System.ComponentModel.DataAnnotations.Schema;

namespace fox_web_api.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        [Column(TypeName = "money")]
        public decimal PaymentAmount { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
