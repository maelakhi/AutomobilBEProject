﻿using FinalProjectCodingIDBE.DTOs.OrderDTO;

namespace FinalProjectCodingIDBE.DTOs.InvoiceDTO
{
    public class InvoiceAdminResposeDTO
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdUser { get; set; }
        public string Status { get; set; } = string.Empty;
        public int totalCourse { get; set; }
        public int totalPrice { get; set; }
    }
}
