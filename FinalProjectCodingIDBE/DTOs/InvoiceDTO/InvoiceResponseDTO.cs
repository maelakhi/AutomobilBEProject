using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.Models;

namespace FinalProjectCodingIDBE.DTOs.InvoiceDTO
{
    public class InvoiceResponseDTO
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int TotalAmount { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public List<OrderDetailsInvoice>? OrderDetails { get; set; }
    }
}
