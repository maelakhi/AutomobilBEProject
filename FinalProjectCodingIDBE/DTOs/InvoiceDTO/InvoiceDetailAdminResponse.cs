using FinalProjectCodingIDBE.Models;

namespace FinalProjectCodingIDBE.DTOs.InvoiceDTO
{
    public class InvoiceDetailAdminResponse
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdPayment { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public int TotalAmount { get; set; }
        public string StatusPayment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailsInvoice>? OrderDetails { get; set; }
    }
}
