using FinalProjectCodingIDBE.DTOs.CartDTO;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectCodingIDBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService serviceCart)
        {
            _cartService = serviceCart;
        }

        [HttpGet("/carts/{userId}")]
        public ActionResult GetAll(int userId)
        {
            return Ok(_cartService.GetCartAll(userId));
        }

        [HttpPost("/carts")]
        public ActionResult AddToCart([FromBody] AddCartDTO cartData )
        {
            string res = _cartService.CartCreate(cartData);
            if(string.IsNullOrEmpty(res) == false)
            {
                return BadRequest(res);
            }
            return Ok("Succesfuly Add To Cart");
        }

        [HttpDelete("/carts/{userId}/{idCart}")]
        public ActionResult DeleteById(int userId, int idCart)
        {
            string res = _cartService.CartDelete(userId, idCart);

            if(string.IsNullOrEmpty (res) == false)
            {
                return BadRequest(res);
            }
            return Ok("SuccessFully Delete Cart");
        }

     /*   [HttpDelete("/carts/{userId}/{idProduct}")]
        public ActionResult DeleteAll(int userId, int idProduct)
        {
            return Ok(_cartService.CartCreate(idProduct, userId));
        }*/
    }
}
