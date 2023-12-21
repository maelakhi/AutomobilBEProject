﻿using System.Security.Cryptography;
using System.Text;

namespace FinalProjectCodingIDBE.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string rawPassword)
        {
            //From String to byte array
            byte[] sourceBytes = Encoding.UTF8.GetBytes(rawPassword);
            byte[] hashBytes = SHA1.HashData(sourceBytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            return hash;
        }
    }
}