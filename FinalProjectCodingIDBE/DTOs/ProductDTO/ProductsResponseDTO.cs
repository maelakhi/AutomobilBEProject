namespace FinalProjectCodingIDBE.DTOs.ProductDTO
{
    public class ProductsResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public int IdCategory { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
