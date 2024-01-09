using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.Models;

namespace FinalProjectCodingIDBE.DTOs.InvoiceDTO
{
    public class InvoiceResponseDTO
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } 
        public List<OrderDetailsInvoice>? OrderDetails { get; set; }
    }
}
