using AuthService.Datas;
using Core;
using Core.GeneralResult;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

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

        //public bool ValidateToken(string token)
        //{
        //    if (token == null)
        //        return false;

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"]);
        //    try
        //    {
        //        tokenHandler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        //            ClockSkew = TimeSpan.Zero
        //        }, out SecurityToken validatedToken);
        //        var jwtToken = (JwtSecurityToken)validatedToken;
        //        if (jwtToken.Claims.Any(x => x.Type == "key"))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}
    }
}
