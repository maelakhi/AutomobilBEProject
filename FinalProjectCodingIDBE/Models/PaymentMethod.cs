namespace FinalProjectCodingIDBE.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public string ImagePath { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}
