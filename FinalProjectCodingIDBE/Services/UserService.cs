﻿using FinalProjectCodingIDBE.Dto.Auth;
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

        public string CreateAccount(RegisterDto data)
        { 
            return _userRepository.CreateAccount(data);
        }
    }
}
