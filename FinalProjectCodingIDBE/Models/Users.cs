namespace FinalProjectCodingIDBE.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public bool IsActive { get; set; }
        public string Role { get; set; } = string.Empty;
        public string VerificationToken { get; set; } = string.Empty;
        public DateTime VerificationExpiredToken { get; set; }
        public bool IsVerified { get; set; }

        public string ResetPasswordToken { get; set; } = string.Empty;
        public DateTime ResetPasswordTokenExpired { get; set; }
    }
}
