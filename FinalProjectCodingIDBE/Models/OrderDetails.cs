﻿namespace FinalProjectCodingIDBE.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public int AmountProduct { get; set; }
        public int TotalAmount { get; set; }
<<<<<<< Updated upstream
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
=======
        public string DateSchedule { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
>>>>>>> Stashed changes
    }
}
