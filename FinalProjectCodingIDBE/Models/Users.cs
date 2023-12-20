namespace FinalProjectCodingIDBE.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
