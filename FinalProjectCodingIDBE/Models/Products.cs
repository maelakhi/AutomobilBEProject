namespace FinalProjectCodingIDBE.Models
{
    public class Products
    {
        public int Id { get; set;}
        public string Name { get; set;} = string.Empty;
        public string Description { get; set;} = string.Empty; 
        public int Price { get; set;}
        public string ImagePath { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public string UpdatedAt { get; set;} = string.Empty;
        public int IdCategory { get; set; }
        public bool IsActive { get; set; } 
    }
}
