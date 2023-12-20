﻿using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectCodingIDBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly PaymentMethodService _paymentService;

        public PaymentMethodController(PaymentMethodService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("/paymentMethod")]
        public IActionResult GetAll()
        {
            List<PaymentMethod> paymentList = _paymentService.GetPaymentMethodAll();
            if (paymentList.Count < 1)
            {
                return NotFound("Data Tidak di temukan");
            }
            return Ok(paymentList);
        }
    }
}
