﻿namespace FinalProjectCodingIDBE.Models
{
    public class Category
    {
        public int Id { get; set;}
        public string Name { get; set;} = string.Empty;
        public string Description { get; set;} = string.Empty; 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}
