using FinalProjectCodingIDBE.DTOs.ProductDTO;
using FinalProjectCodingIDBE.Models;

namespace FinalProjectCodingIDBE.DTOs.CartDTO
{
    public class CartResponseDTO
    {
        public int Id { get; set; }

        public int IdUser { get; set; }
        public int IdProduct { get; set; }

        public ProductsResponseDTO? product { get; set; }
        public string DateSchedule { get; set; } = string.Empty;
    }
}
