using FinalProjectCodingIDBE.Dto.Auth;
using FinalProjectCodingIDBE.DTOs.AuthDTO;
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
        public async Task<ActionResult> Register([FromBody] RegisterDto data)
        {
            Users? userExist = _userService.GetByEmail(data.Email);

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
            string verificationToken = Guid.NewGuid().ToString();

            data.Password = hashedPassword;

            string res = _userService.CreateAccount(data, verificationToken);

            if (!string.IsNullOrEmpty(res))
            {
                return StatusCode(
                        (int)HttpStatusCode.Accepted,
                        new
                        {
                            status = HttpStatusCode.Accepted,
                            message = "Create Account Failed!"
                        }
                    );
            }

            string htmlEmail = $@"
                                Hello <b>{data.Email}</b>, please click link below to verify<br/>
                                <a href='http://localhost:5173/confirmationEmail/{verificationToken}'>
                                    <button>Verify My Account</botton>
                                </a>
                                ";

            await MailHelper.Send("Dear User", data.Email, "Email Verification", htmlEmail);


            return StatusCode(
                        (int)HttpStatusCode.OK,
                        new
                        {
                            status = HttpStatusCode.OK,
                            message = "Create Account Successfull!"
                        }
                    );

        }

        [HttpPost("/verifiedEmail")]
        public ActionResult VerifiedAccount([FromBody] VerifiedDTO data)
        {
            Users? userExits = _userService.GetAccountByToken(data);

            if (userExits.Id == 0)
            {
                return StatusCode(
                       (int)HttpStatusCode.NotFound,
                       new
                       {
                           status = HttpStatusCode.NotFound,
                           message = "Token invalid"
                       }
                   );
            }
/*
            if (userExits.VerificationExpiredToken <= DateTime.Now)
            {
                return StatusCode(
                       (int)HttpStatusCode.Accepted,
                       new
                       {
                           status = HttpStatusCode.Accepted,
                           message = "Token is Expired"
                       }
                   );
            }*/

            string res = _userService.SetAccountVerified(userExits.Id);

            if (!string.IsNullOrEmpty(res))
            {
                return StatusCode(
                       (int)HttpStatusCode.NotFound,
                       new
                       {
                           status = HttpStatusCode.NotFound,
                           message = res
                       }
                   );
            }

            return StatusCode(
                       (int)HttpStatusCode.OK,
                       new
                       {
                           status = HttpStatusCode.OK,
                           message = "Account Verified"
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