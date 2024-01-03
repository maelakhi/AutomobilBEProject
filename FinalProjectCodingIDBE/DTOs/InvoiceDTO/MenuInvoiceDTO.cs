using FinalProjectCodingIDBE.DTOs.OrderDTO;

namespace FinalProjectCodingIDBE.DTOs.InvoiceDTO
{
    public class MenuInvoiceDTO
    {
        public int Id { get; set; }
        public string createdAt { get; set; } = string.Empty;
        public int totalCourse { get; set; }
        public int totalPrice { get; set; }
    }
}
