using System.ComponentModel;

namespace FinalProjectCodingIDBE.Dto.Auth
{
    public class RegisterDto
    {
        [DefaultValue("testing@email.com")]
        public string Email { get; set; } = string.Empty;
        [DefaultValue("password")]  
        public string Password { get; set; } = string.Empty;
    }
}