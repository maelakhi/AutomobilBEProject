using FinalProjectCodingIDBE.DTOs.ProductDTO;
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
        public List<ProductsResponseDTO> GetAllProducts()
        {
            List<ProductsResponseDTO> products = _productsRepository.GetProductsAll();
            return products;
        }

        public ProductsResponseDTO GetByIdProducts(int Id)
        {
            ProductsResponseDTO product = _productsRepository.GetProductsById(Id);
            return product;
        }
        public Products ProductCreate(AddProductsDTO productsDTO)
        {
            Products product = _productsRepository.CreateProduct(productsDTO);
            return product;
        }
        public Products ProductUpdate(int Id, AddProductsDTO productsDTO)
        {
            Products product = _productsRepository.UpdateProduct(Id,productsDTO);
            return product;
        }
        public bool ProductDelete(int Id)
        {
            bool flagDelete = _productsRepository.DeleteProduct(Id);
            return flagDelete;
        }

        /*Landing Page*/
        public List<ProductsResponseDTO> GetLimitProducts()
        {
            List<ProductsResponseDTO> products = _productsRepository.GetProductsLimit();
            return products;
        }
        public List<ProductsResponseDTO> GetByCategoryProducts(String categoryName)
        {
            List<ProductsResponseDTO> products = _productsRepository.GetProductsByCategory(categoryName);
            return products;
        }
    }
}
