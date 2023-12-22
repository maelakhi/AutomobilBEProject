namespace FinalProjectCodingIDBE.DTOs.AuthDTO
{
    public class CreateNewPasswordDTO
    {
        public OTPverifiedDTO DTOkey { get; set; }
        public string Password { get; set; } 
        public string ConfirmPassword { get; set; }
    }
}
