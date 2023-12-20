using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Repositories;

namespace FinalProjectCodingIDBE.Services
{
    public class PaymentMethodService
    {
        private readonly PaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService(PaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public List<PaymentMethod> GetPaymentMethodAll()
        {
            return _paymentMethodRepository.PaymentList();
        }
    }
}
