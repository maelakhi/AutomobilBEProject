using FinalProjectCodingIDBE.DTOs.OrderDTO;

namespace FinalProjectCodingIDBE.DTOs.InvoiceDTO
{
    public class InvoiceResponseDTO
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public string Status { get; set; } = string.Empty;
        public string createdAt { get; set; } = string.Empty;
        public string updatedAt { get; set; } = string.Empty;

        public OrderResponseDTO? OrderResponse { get; set; }
    }
}
