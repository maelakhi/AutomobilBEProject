using FinalProjectCodingIDBE.DTOs.OrderDTO;

namespace FinalProjectCodingIDBE.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public string Status { get; set; } = string.Empty;
        public string createdAt { get; set; } = string.Empty;
        public string updatedAt { get; set; } = string.Empty;
    }
}
