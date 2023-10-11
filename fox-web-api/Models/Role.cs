namespace fox_web_api.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string SystemRole { get; set; }
        List<User>? Users { get; set; }
    }
}
