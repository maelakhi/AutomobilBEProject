namespace FinalProjectCodingIDBE.DTOs.UsersDTO
{
    public class EditUserAdminDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
