using System.ComponentModel;

namespace FinalProjectCodingIDBE.Dto.Auth
{
    public class LoginDto
    {
        [DefaultValue("developprz@gmail.com")]
        public string Email { get; set; } = string.Empty;
        [DefaultValue("Testing123@")]
        public string Password { get; set; } = string.Empty;
    }
}