using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FinalProjectCodingIDBE.Helpers
{
    public static class JWTHelper
    {
        public static string KEY = "527ee0a771c8a45e458b60f1db7598b04bcde720a84af306f16d6b194189e569a60c2c37d3b7fdff2e546b5b62fba71a36e54f1d79d4cee2888a36f6ed93fca1";
        public static string Generate(int userId, string role)
        {
            //init claims payload
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Sid, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
            };

            //set jwt config
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY)), SecurityAlgorithms.HmacSha512)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}