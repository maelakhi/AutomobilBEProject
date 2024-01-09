namespace FinalProjectCodingIDBE.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdPayment { get; set; }
        public int TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
    }
}
