using FinalProjectCodingIDBE.Models;

namespace FinalProjectCodingIDBE.DTOs.CartDTO
{
    public class AddCartDTO
    {
        public int IdProduct { get; set; }
        public int IdUser { get; set; }
        public string DateSchedule { get; set; } = string.Empty;
    }
}
