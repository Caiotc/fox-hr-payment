using fox_web_api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace fox_web_api.Dtos
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
    }
}
