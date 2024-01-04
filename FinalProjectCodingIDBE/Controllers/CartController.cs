using FinalProjectCodingIDBE.DTOs.CartDTO;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet("/carts")]
        public ActionResult GetAll()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            return Ok(_cartService.GetCartAll(userId));
        }

        [Authorize]
        [HttpGet("/carts/{idCart}")]
        public ActionResult GetCartById(int idCart)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            return Ok(_cartService.GetCartById(userId, idCart));
        }

        [Authorize]
        [HttpPost("/carts")]
        public ActionResult AddToCart([FromBody] AddCartDTO cartData )
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            cartData.IdUser = userId;

            string res = _cartService.CartCreate(cartData);

            if(string.IsNullOrEmpty(res) == false)
            {
                return StatusCode(
                         (int)HttpStatusCode.NotFound,
                            new
                            {
                               status = HttpStatusCode.NotFound,
                               message = "Failed Add to Cart"
                            }
                       );
            }

            return StatusCode(
                      (int)HttpStatusCode.OK,
                      new
                      {
                          status = HttpStatusCode.OK,
                          message = "Succesfuly Add To Cart"
                      }
                  );
        }

        [Authorize]
        [HttpPost("/cartsBuyNow")]
        public ActionResult AddToCartBuyNow([FromBody] AddCartDTO cartData)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            cartData.IdUser = userId;

            string res = _cartService.CartCreateBuyNow(cartData);

            if (string.IsNullOrEmpty(res) == true)
            {
                return StatusCode(
                         (int)HttpStatusCode.NotFound,
                            new
                            {
                                status = HttpStatusCode.NotFound,
                                message = "Failed Preaparing Order"
                            }
                       );
            }

            return StatusCode(
                      (int)HttpStatusCode.OK,
                      new
                      {
                          status = HttpStatusCode.OK,
                          message = "Succesfuly Preaparing Order",
                          data = res
                      }
                  );
        }

        [Authorize]
        [HttpDelete("/carts/{idCart}")]
        public ActionResult DeleteById(int idCart)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));

            string res = _cartService.CartDelete(userId, idCart);

            if (string.IsNullOrEmpty(res) == false)
            {
                return StatusCode(
                         (int)HttpStatusCode.NotFound,
                            new
                            {
                                status = HttpStatusCode.NotFound,
                                message = "Delete Failed"
                            }
                       );
            }

            return StatusCode(
                      (int)HttpStatusCode.OK,
                      new
                      {
                          status = HttpStatusCode.OK,
                          message = "Succesfuly Delete from Cart"
                      }
                  );
        }

    }
}
