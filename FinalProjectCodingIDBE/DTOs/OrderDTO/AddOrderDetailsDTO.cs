namespace FinalProjectCodingIDBE.DTOs.OrderDTO
{
    public class AddOrderDetailsDTO
    {
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public int AmountProduct { get; set; }
        public int TotalAmount { get; set; }
    }
}
