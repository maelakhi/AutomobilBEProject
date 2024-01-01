using FinalProjectCodingIDBE.DTOs.InvoiceDTO;
using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Repositories;

namespace FinalProjectCodingIDBE.Services
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _repositoryInvoice;
        public InvoiceService(InvoiceRepository invoiceRepository)
        {
            _repositoryInvoice = invoiceRepository;
        }

        public List<MenuInvoiceDTO> GetInvoiceAll(int userId)
        {
            return _repositoryInvoice.GetInvoiceAll(userId);
        }

        public InvoiceResponseDTO GetInvoiceById(int userId, int idInvoice)
        {
            return _repositoryInvoice.GetInvoiceById(userId, idInvoice);
        }

        public string CreateInvoice(int userId, int idOrder)
        {
            return _repositoryInvoice.CreateInvoice(userId, idOrder);
        }

        public List<InvoiceAdminResposeDTO> GetInvoiceAllAdmin()
        {
            return _repositoryInvoice.GetInvoiceAllAdmin();
        }
        public InvoiceDetailAdminResponse GetInvoiceByIdAdmin(int Id)
        {
            return _repositoryInvoice.GetInvoiceByIdAdmin(Id);
        }
    }
}
