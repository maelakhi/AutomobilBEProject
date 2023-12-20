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
        public ActionResult Login([FromBody] LoginDto data)
        {
            string hashedPassword = PasswordHelper.HashPassword(data.Password);

            Users? user = _userRepository.GetByEmailAndPassword(data.Email, data.Password);

            if (user == null)
            {
                return NotFound();
            }

            //create token
            string token = JWTHelper.Generate(user.Id, user.Role);

            return Ok(token);
        }
    }
}