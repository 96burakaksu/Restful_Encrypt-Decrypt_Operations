using AuthService.DTOs;
using Core;
using Core.GeneralResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private IJWTAuthenticationManager _jWTAuthenticationManager;
        private readonly IConfiguration _config;

     
        public AuthController(IJWTAuthenticationManager jWTAuthenticationManager, IConfiguration config)
        {
            _jWTAuthenticationManager = jWTAuthenticationManager;
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        
        public IActionResult Authenticate(Users usersdata)
        {
            var token = _jWTAuthenticationManager.Authenticate(usersdata);

            if (token.StatusCode==401)
            {
                return Unauthorized(token);
            }

            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Deneme()
        {
            var users = new DataResult<string> { Data = "yalan dolan" };
            return Ok(users);
        }

    }
  



}
