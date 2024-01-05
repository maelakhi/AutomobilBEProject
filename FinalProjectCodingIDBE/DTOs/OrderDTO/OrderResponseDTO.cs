using FinalProjectCodingIDBE.Models;

namespace FinalProjectCodingIDBE.DTOs.OrderDTO
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdPayment { get; set; }
        public int TotalAmount { get; set; }
<<<<<<< Updated upstream
        public string StatusPayment { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
=======
        public string StatusPayment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
>>>>>>> Stashed changes
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}
