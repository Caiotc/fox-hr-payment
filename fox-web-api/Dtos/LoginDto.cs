using fox_web_api.Models;

namespace fox_web_api.Dtos
{
    public class LoginDto :UserDto
    {
        public int Id { get; set; }
        public string JwtBearer { get; set; }

        public string UserPhoto { get; set; }

        public Role Role { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

       
    }
}
