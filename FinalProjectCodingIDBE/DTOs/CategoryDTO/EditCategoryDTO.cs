namespace FinalProjectCodingIDBE.DTOs.CategoryDTO
{
    public class EditCategoryDTO
    {
        public int categoryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }

    }
}
