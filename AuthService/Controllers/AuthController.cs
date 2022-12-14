using AuthService.DTOs;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IJWTAuthenticationManager _jWTAuthenticationManager;

        public AuthController(IJWTAuthenticationManager jWTAuthenticationManager)
        {
            _jWTAuthenticationManager = jWTAuthenticationManager;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users usersdata)
        {
            var token = _jWTAuthenticationManager.Authenticate(usersdata);

            if (token.StatusCode==401)
            {
                return Unauthorized(token);
            }

            return Ok(token);
        }



    }
  



}
