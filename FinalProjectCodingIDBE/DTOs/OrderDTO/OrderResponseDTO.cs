using FinalProjectCodingIDBE.Models;

namespace FinalProjectCodingIDBE.DTOs.OrderDTO
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdPayment { get; set; }
        public int TotalAmount { get; set; }
        public string StatusPayment { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}
