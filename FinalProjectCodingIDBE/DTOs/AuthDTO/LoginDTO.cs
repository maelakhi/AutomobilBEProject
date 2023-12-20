using System.ComponentModel;

namespace FinalProjectCodingIDBE.Dto.Auth
{
    public class LoginDto
    {
        [DefaultValue("testing@email.com")]
        public string Email { get; set; } = string.Empty;
        [DefaultValue("password")]  
        public string Password { get; set; } = string.Empty;
    }
}