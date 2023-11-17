using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Data
{
    public class ChangePasswordModel
    {
        [Required]
        public string oldPassword { get; set; }
        [Required]
        public string newPassord { get; set; }
    }
}
