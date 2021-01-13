using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Data;
using ApiParking.Data.User;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ApiParking.Handler
{
    public class JwtAuthentication : IJwtAuthenticationManager
    {
        
        private readonly string _key;
        public JwtAuthentication(string key)
        {
            _key = key;
        }
        public string Authenticate(string username, string password, int id)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, username)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
