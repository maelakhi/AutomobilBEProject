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

        public List<ProductsResponseDTO> GetAllProductsActived()
        {
            List<ProductsResponseDTO> products = _productsRepository.GetAllProductsActived();
            return products;
        }

        public ProductsResponseDTO GetByIdProducts(int Id)
        {
            ProductsResponseDTO product = _productsRepository.GetProductsById(Id);
            return product;
        }

        public String ProductCreate(AddProductsDTO productsDTO, string imageFilePath)
        {
            String product = _productsRepository.CreateProduct(productsDTO, imageFilePath);
            return product;
        }

        public string ProductUpdate(int Id, EditProductsDTO productsDTO, string imageFilePath)
        {
            string product = _productsRepository.UpdateProduct(Id,productsDTO,imageFilePath);
            return product;
        }

        public string ProductDelete(int Id)
        {
            string flagDelete = _productsRepository.DeleteProduct(Id);
            return flagDelete;
        }

        public string ProductUpdateStatus(int Id, bool Status)
        {
            string product = _productsRepository.UpdateStatusProduct(Id, Status);
            return product;
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

        public List<ProductsResponseDTO> GetByCategoryProductsId(int Id)
        {
            List<ProductsResponseDTO> products = _productsRepository.GetProductsByCategoryId(Id);
            return products;
        }
    }
}
