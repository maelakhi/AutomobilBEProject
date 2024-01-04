namespace FinalProjectCodingIDBE.DTOs.PaymentDTO
{
    public class EditPaymentDTO
    {
        public int paymentID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }
    }
}
