using FinalProjectCodingIDBE.Dto.Auth;
using FinalProjectCodingIDBE.DTOs.UsersDTO;
using FinalProjectCodingIDBE.Helpers;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Repositories;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinalProjectCodingIDBE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/Login")]
        public ActionResult Login([FromBody] LoginDto data)
        {
            string hashedPassword = PasswordHelper.HashPassword(data.Password);

            data.Password = hashedPassword;

            Users? user = _userService.GetByEmailAndPassword(data);

            if (user == null)
            {
                return StatusCode(
                        (int)HttpStatusCode.Accepted, 
                        new { 
                            status = HttpStatusCode.Accepted,
                            message = "Password atau Email tidak valid"
                        }
                    );
            }

            //create token
            string token = JWTHelper.Generate(user.Id, user.Role);

            return StatusCode(
                        (int)HttpStatusCode.OK,
                        new
                        {
                            status = HttpStatusCode.OK,
                            message = "Berhasil Login",
                            data = new { Token = token, Role = user.Role }
                        }
                    );
        }

        [HttpPost("/Register")]
        public ActionResult Register([FromBody] RegisterDto data)
        {
            Users? userExist = _userService.GetByEmail(data.Email);

            Console.WriteLine(userExist?.Email);
            if(userExist != null)
            {
               return StatusCode(
                       (int)HttpStatusCode.Accepted,
                       new
                       {
                           status = HttpStatusCode.Accepted,
                           message = "Email sudah terdaftar"
                       }
                   );
            }

            string hashedPassword = PasswordHelper.HashPassword(data.Password);

            data.Password = hashedPassword;

            string res = _userService.CreateAccount(data);

            if (!string.IsNullOrEmpty(res))
            {
                StatusCode(
                        (int)HttpStatusCode.Accepted,
                        new
                        {
                            status = HttpStatusCode.Accepted,
                            message = res
                        }
                    );
            }

            return StatusCode(
                        (int)HttpStatusCode.OK,
                        new
                        {
                            status = HttpStatusCode.OK,
                            message = "Create Account Successfull!"
                        }
                    );

        }

        [HttpGet("/users")]
        public ActionResult Register()
        {
            List<UserResponseDTO> userList = _userService.GetUserAll();

            if (userList.Count <= 0)
            {
                StatusCode(
                        (int)HttpStatusCode.Accepted,
                        new
                        {
                            status = HttpStatusCode.Accepted,
                            message = "Data tidak ada"
                        }
                    );
            }

            return Ok(userList);

        }

    }
}