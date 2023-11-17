using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Data
{
    public class Payment
    {
        public int id { get; set; }
        public DateTime paymentDate { get; set; }
        public decimal paymentAmount { get; set; }
        public int? employeeId { get; set; }
        public Employee? employee { get; set; }
    }
}
