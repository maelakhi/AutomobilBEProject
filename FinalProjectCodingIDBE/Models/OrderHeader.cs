﻿namespace FinalProjectCodingIDBE.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdPayment { get; set; }
        public int TotalAmount { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
    }
}
