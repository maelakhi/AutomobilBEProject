using FinalProjectCodingIDBE.Dto.Auth;
using FinalProjectCodingIDBE.DTOs.AuthDTO;
using FinalProjectCodingIDBE.DTOs.DashBoardDTO;
using FinalProjectCodingIDBE.DTOs.UsersDTO;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Repositories;

namespace FinalProjectCodingIDBE.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserResponseDTO> GetUserAll()
        {
            return _userRepository.GetUserAll();
        }

        public Users? GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public Users? GetByEmailAndPassword(LoginDto data)
        {
            return _userRepository.GetByEmailAndPassword(data);
        }

        public string CreateAccount(RegisterDto data, string verificationToken)
        {
            return _userRepository.CreateAccount(data, verificationToken);
        }

        public Users? GetAccountByToken(VerifiedDTO data)
        {
            return _userRepository.GetAccountByToken(data);
        }
        public string SetAccountVerified(int Id)
        {
            return _userRepository.SetAccountVerified(Id);
        }

        public string CreateOTPCode(string email, string otpCode)
        {
            return _userRepository.CreateOTPCode(email, otpCode);
        }
        public Users? GetByOTPCode(OTPverifiedDTO data)
        {
            return _userRepository.GetByOTPCode(data);
        }
        public string SetNewPassword(CreateNewPasswordDTO data,int Id)
        {
            return _userRepository.SetNewPassword(data, Id);
        }

        public List<ChartUsers> GetDashboardUsers()
        {
            return _userRepository.GetDashboardUsers();
        }

    }
}