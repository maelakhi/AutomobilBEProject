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
/*
        [HttpGet("/carts/{userId}/{idProduct}")]
        public ActionResult GetAll(int userId,int  idProduct)
        {
            return Ok(_cartService.GetCartById(idProduct,userId));
        }

        [HttpPost("/carts")]
        public ActionResult AddToCart([FromBody] Cart cartData )
        {
            return Ok(_cartService.CartCreate(cartData));
        }

        [HttpDelete("/carts/{userId}/{idProduct}")]
        public ActionResult DeleteById(int userId, int idProduct)
        {
            return Ok(_cartService.GetCartById(idProduct, userId));
        }*/
/*
        [HttpDelete("/carts/{userId}/{idProduct}")]
        public ActionResult DeleteAll(int userId, int idProduct)
        {
            return Ok(_cartService.CartCreate(idProduct, userId));
        }*/
    }
}
