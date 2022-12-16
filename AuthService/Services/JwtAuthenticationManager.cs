using AuthService.Datas;
using AuthService.DTOs;
using Core;
using Core.GeneralResult;
using Microsoft.Extensions.Configuration;
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

        
        DefinedUsers definedUsers = new DefinedUsers();
        private readonly IConfiguration _configuration;
        public JwtAuthenticationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataResult<string> Authenticate(Users user)
        {

            if (user.Key != null && user.Key != null)
            {
                if (!definedUsers.users.Any(x => x.Key == user.Key && x.Value == user.Pass))
                {
                    return DataResult<string>.Fail(401, "User information is incorrect");
                }

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Key)
                    }),
                    Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtToken:TokenExpiry"])),
                    Issuer = _configuration["JwtToken:Issuer"],
                    Audience = _configuration["JwtToken:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"])), SecurityAlgorithms.HmacSha256Signature)

                };


                //Token creating.
           
                var token = tokenHandler.CreateToken(tokenDescriptor);
                if (token == null)
                {
                    return DataResult<string>.Fail(401, "Failed to create token");
                }
                return DataResult<string>.SuccessData(tokenHandler.WriteToken(token));
            }

            return DataResult<string>.Fail(401, "Username or Password cannot be blank");

        }

    }
}
