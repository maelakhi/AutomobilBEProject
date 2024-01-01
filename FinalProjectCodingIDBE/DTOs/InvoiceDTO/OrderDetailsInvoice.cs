using FinalProjectCodingIDBE.DTOs.ProductDTO;

namespace FinalProjectCodingIDBE.DTOs.InvoiceDTO
{
    public class OrderDetailsInvoice
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public ProductsResponseDTO? product  { get; set; }
        public int Quantity { get; set; }
        public int AmountProduct { get; set; }
        public int TotalAmount { get; set; }
        public DateTime DateSchedule { get; set; } 
    }
}
