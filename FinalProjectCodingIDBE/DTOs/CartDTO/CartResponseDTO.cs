using FinalProjectCodingIDBE.Models;

namespace FinalProjectCodingIDBE.DTOs.CartDTO
{
    public class CartResponseDTO
    {
        public int Id { get; set; }
        public Products product { get; set; }

        public int IdUser { get; set; }
    }
}
