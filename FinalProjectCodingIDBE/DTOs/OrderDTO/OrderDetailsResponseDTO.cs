using FinalProjectCodingIDBE.DTOs.ProductDTO;

namespace FinalProjectCodingIDBE.DTOs.OrderDTO
{
    public class OrderDetailsResponseDTO
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public ProductsResponseDTO? Product { get; set; }
        public DateTime DateSchedule { get; set; }
    }
}
