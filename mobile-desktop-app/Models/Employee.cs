using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobile_desktop_app.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string address { get; set; }

        public decimal income { get; set; }
        public DateTime admissionDate { get; set; }
        public DateTime birthDate { get; set; }

        public string position { get; set; }

        public string? userPhoto { get; set; }
        public int departmentId { get; set; }
    }
}
