using AuthService.Datas;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService
{
    public class JwtAuthenticationManager : IJWTAuthenticationManager
    {
       
        private readonly byte[] _mySecret = Encoding.ASCII.GetBytes("It is a secret created for Procenne");
        DefinedUsers definedUsers = new DefinedUsers();

       

        public string Authenticate(string key, string pass)
        {
            if(!definedUsers.users.Any(x => x.Key == key && x.Value == pass))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, key)
                }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_mySecret),SecurityAlgorithms.HmacSha256Signature)
               
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            
        }
    }
}
