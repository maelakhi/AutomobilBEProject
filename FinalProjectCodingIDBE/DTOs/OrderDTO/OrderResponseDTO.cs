﻿using FinalProjectCodingIDBE.Models;

namespace FinalProjectCodingIDBE.DTOs.OrderDTO
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdPayment { get; set; }
        public int TotalAmount { get; set; }
        public string StatusPayment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}
