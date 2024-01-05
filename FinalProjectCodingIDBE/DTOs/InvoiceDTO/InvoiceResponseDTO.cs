using FinalProjectCodingIDBE.DTOs.OrderDTO;

namespace FinalProjectCodingIDBE.DTOs.InvoiceDTO
{
    public class InvoiceResponseDTO
    {
        public int Id { get; set; }
<<<<<<< Updated upstream
        public int IdOrder { get; set; }
        public string Status { get; set; } = string.Empty;
        public string createdAt { get; set; } = string.Empty;
        public string updatedAt { get; set; } = string.Empty;

        public OrderResponseDTO? OrderResponse { get; set; }
=======
        public int IdUser { get; set; }
        public int TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailsInvoice>? OrderDetails { get; set; }
>>>>>>> Stashed changes
    }
}
