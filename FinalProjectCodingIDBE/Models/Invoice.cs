using FinalProjectCodingIDBE.DTOs.OrderDTO;

namespace FinalProjectCodingIDBE.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime createdAt { get; set; } 
        public DateTime updatedAt { get; set; } 
    }
}
