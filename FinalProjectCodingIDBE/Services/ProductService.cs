using FinalProjectCodingIDBE.DTOs;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Repositories;

namespace FinalProjectCodingIDBE.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productsRepository;
        public ProductService(ProductRepository productRepository) {
            _productsRepository = productRepository;
        }
        public List<ProductsDTO> GetAllProducts()
        {
            return _productsRepository.GetProductsAll();
        }
    }
}
