using FinalProjectCodingIDBE.Dto.Auth;
using FinalProjectCodingIDBE.DTOs.AuthDTO;
using FinalProjectCodingIDBE.DTOs.DashBoardDTO;
using FinalProjectCodingIDBE.DTOs.UsersDTO;
using FinalProjectCodingIDBE.Helpers;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Repositories;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;

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
            string validationPass = ValidationHelper.ValidationPassword(data.Password);
       
            if (!string.IsNullOrEmpty(validationPass))
            {
                return StatusCode(
                        (int)HttpStatusCode.Accepted,
                        new
                        {
                            status = HttpStatusCode.Accepted,
                            message = validationPass
                        }
                    );
            }

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
            /*Validasi Password*/
            string validationPass = ValidationHelper.ValidationPassword(data.Password);
            if (!string.IsNullOrEmpty(validationPass))
            {
                return StatusCode(
                        (int)HttpStatusCode.Accepted,
                        new
                        {
                            status = HttpStatusCode.Accepted,
                            message = validationPass
                        }
                    );
            }

            /*Validasi Confirm Password*/
            string validationPassCon = ValidationHelper.ValidationConfirmPassword(data.Password, data.ConfirmPassword);
            if (!string.IsNullOrEmpty(validationPassCon))
            {
                return StatusCode(
                        (int)HttpStatusCode.Accepted,
                        new
                        {
                            status = HttpStatusCode.Accepted,
                            message = validationPassCon
                        }
                    );
            }

            Users? userExist = _userService.GetByEmail(data.Email);

            if (userExist != null)
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

        [HttpPost("/resetPassword")]
        public async Task<ActionResult> ResetPasssword([FromBody] ResetPasswordDTO data)
        {
            Users? userExits = _userService.GetByEmail(data.Email);

            if (userExits == null)
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

            string OTPCode = OTPHelper.GenerateRandomOTP();
            
            string res = _userService.CreateOTPCode(userExits.Email, OTPCode);

            if (!string.IsNullOrEmpty(res))
            {
                return StatusCode(
                       (int)HttpStatusCode.Accepted,
                       new
                       {
                           status = HttpStatusCode.Accepted,
                           message = res
                       }
                   );
            }

           string htmlEmail = $@"
                                Hello <b>{data.Email}</b>, please keep the OTP code secrete<br/>
                                <div style='padding:5px;backgroundColor:blue;c'>
                                    {OTPCode}
                                 </div>
                                ";

            await MailHelper.Send("Dear User", data.Email, "Reset Password OTP", htmlEmail);

            return StatusCode(
                       (int)HttpStatusCode.OK,
                       new
                       {
                           status = HttpStatusCode.OK,
                           message = "OTP Success Create"
                       }
                   );

        }


        [HttpPost("/verifiedOTPCode")]
        public ActionResult verifiedOTP([FromBody] OTPverifiedDTO data)
        {
            Users? userExist = _userService.GetByOTPCode(data);

            if (userExist == null)
            {
                return StatusCode(
                       (int)HttpStatusCode.Accepted,
                       new
                       {
                           status = HttpStatusCode.Accepted,
                           message = "OTP Invalid"
                       }
                   );
            }

            return StatusCode(
                       (int)HttpStatusCode.OK,
                       new
                       {
                           status = HttpStatusCode.OK,
                           message = "OTP Valid"
                       }
                   );

        }

        [HttpPost("/createNewPassword")]
        public async Task<ActionResult> CreateNewPasseord([FromBody] CreateNewPasswordDTO data)
        {
            /*Validasi Password*/
            string validationPass = ValidationHelper.ValidationPassword(data.Password);
            if (!string.IsNullOrEmpty(validationPass))
            {
                return StatusCode(
                        (int)HttpStatusCode.Accepted,
                        new
                        {
                            status = HttpStatusCode.Accepted,
                            message = validationPass
                        }
                    );
            }

            /*Validasi Confirm Password*/
            string validationPassCon = ValidationHelper.ValidationConfirmPassword(data.Password, data.ConfirmPassword);
            if (!string.IsNullOrEmpty(validationPassCon))
            {
                return StatusCode(
                        (int)HttpStatusCode.Accepted,
                        new
                        {
                            status = HttpStatusCode.Accepted,
                            message = validationPassCon
                        }
                    );
            }

            Users? userExist = _userService.GetByOTPCode(data.DTOkey);

            if (userExist == null)
            {
                return StatusCode(
                        (int)HttpStatusCode.Accepted,
                        new
                        {
                            status = HttpStatusCode.Accepted,
                            message = "Create New Pass Failed"
                        }
                    );
            }

            string hashedPassword = PasswordHelper.HashPassword(data.Password);
            string verificationToken = Guid.NewGuid().ToString();

            data.Password = hashedPassword;

            string res = _userService.SetNewPassword(data, userExist.Id);

            if (!string.IsNullOrEmpty(res))
            {
                return StatusCode(
                        (int)HttpStatusCode.Accepted,
                        new
                        {
                            status = HttpStatusCode.Accepted,
                            message = "Update Password Failed!"
                        }
                    );
            }

            string htmlEmail = $@"
                                Hello <b>{userExist.Email}</b>, Yout password is resert <br/>
                                ";

            await MailHelper.Send("Dear User", userExist.Email, "Change Password", htmlEmail);


            return StatusCode(
                        (int)HttpStatusCode.OK,
                        new
                        {
                            status = HttpStatusCode.OK,
                            message = "Update Password Successfull!"
                        }
                    );
        }


        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpGet("/admin/usersDashboard")]
        public ActionResult GetDashboardUsers()
        {
            List<ChartUsers> userList = _userService.GetDashboardUsers();

            if (userList.Count <= 0)
            {
                return StatusCode(
                        (int)HttpStatusCode.Accepted,
                        new
                        {
                            status = HttpStatusCode.Accepted,
                            message = "Data tidak ada"
                        }
                    );
            }

            return StatusCode(
                        (int)HttpStatusCode.OK,
                        new
                        {
                            status = HttpStatusCode.OK,
                            message = "Successfully Get Data",
                            data = userList
                        }
                    );

        }
    }
}