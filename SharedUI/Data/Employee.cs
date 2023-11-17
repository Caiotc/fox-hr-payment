using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Data
{
    public class Employee
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }
        [Required]

        public string email { get; set; }
        [Required]

        public string cpf { get; set; }
        [Required]

        public string address { get; set; }
        [Required]

        public decimal income { get; set; }
        [Required]

        public DateTime admissionDate { get; set; }
        [Required]

        public DateTime birthDate { get; set; }

        [Required]


        public string position { get; set; }

        public string? userPhoto { get; set; }
        [Required]

        public int departmentId { get; set; }
    }
}
