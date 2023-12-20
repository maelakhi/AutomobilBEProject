using FinalProjectCodingIDBE.Dto.Auth;
using FinalProjectCodingIDBE.Helpers;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectCodingIDBE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("/Login")]
        public ActionResult Login([FromForm] LoginDto data)
        {
            string hashedPassword = PasswordHelper.HashPassword(data.Password);

            Users? user = _userRepository.GetByEmailAndPassword(data.Email, hashedPassword);

            if (user == null)
            {
                return NotFound();
            }

            //create token
            string token = JWTHelper.Generate(user.Id, user.Role);

            return Ok(token);
        }

        [HttpPost("/Register")]
        public ActionResult Register([FromForm] RegisterDto data)
        {
            string hashedPassword = PasswordHelper.HashPassword(data.Password);

            string res = _userRepository.CreateAccount(data.Email, hashedPassword);

            if (!string.IsNullOrEmpty(res))
            {
                return BadRequest(res);
            }

            return Ok("Create Account Successfull!");
        }

    }
}