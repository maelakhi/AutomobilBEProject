namespace FinalProjectCodingIDBE.DTOs.ProductDTO
{
    public class EditProductsDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int IdCategory { get; set; }
        public IFormFile? Image { get; set; }
    }
}
