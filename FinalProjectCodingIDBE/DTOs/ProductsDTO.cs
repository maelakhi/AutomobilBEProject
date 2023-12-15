namespace FinalProjectCodingIDBE.DTOs
{
    public class ProductsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public String CategoryName { get; set; } = string.Empty;
    }
}
