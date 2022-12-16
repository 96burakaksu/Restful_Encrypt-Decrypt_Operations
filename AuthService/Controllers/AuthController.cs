using AuthService.DTOs;
using Core;
using Core.GeneralResult;
using CryptoService.Interfaces;
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
        private ICryptoService _criptoService;


        public AuthController(IJWTAuthenticationManager jWTAuthenticationManager, ICryptoService criptoService)
        {
            _jWTAuthenticationManager = jWTAuthenticationManager;
            _criptoService = criptoService;
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
        [HttpPost]
        public IActionResult Encripto(CriptoRequest request)
        {
            var result = new DataResult<byte[]>();
            if (request.IsEncrypt)
            {
               result= _criptoService.Encrypt(request);
            }
            else
            {
               result= _criptoService.Decrypt(request);
            }
            return Ok(result);
            
        }

     
    }
  



}
