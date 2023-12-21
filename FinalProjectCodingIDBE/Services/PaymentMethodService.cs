using FinalProjectCodingIDBE.DTOs.PaymentDTO;
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
            return _paymentMethodRepository.GetPaymentList();
        }

        public PaymentMethod GetPaymentMethodById(int Id)
        {
            return _paymentMethodRepository.GetPaymentById(Id);
        }

        public string AddPayment(AddPaymentDTO payment)
        {
            return _paymentMethodRepository.AddPaymentMethod(payment);
        }
        public string UpdatePayment(int Id, AddPaymentDTO payment)
        {
            return _paymentMethodRepository.UpdatePaymentMethod(Id, payment);
        }

        public string DeletePayment(int Id)
        {
            return _paymentMethodRepository.DeletePaymentMethod(Id);
        }
    }
}
