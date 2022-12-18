using Core;
using Core.GeneralResult;
using CryptoService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoService.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CryptionController : ControllerBase
    {
        private ICryptoService _criptoService;

        public CryptionController(ICryptoService criptoService)
        {
         
            _criptoService = criptoService;
        }

        [HttpPost]
        public IActionResult Encripto(CriptoDataRequest request)
        {
         
                var result = _criptoService.Encrypt(request);
            result.StatusCode = 200;
            return Ok(result);

        }
        [HttpPost]
        public IActionResult Decripto(CriptoDataRequest request)
        {

            var result = _criptoService.Decrypt(request);

            return Ok(result);

        }
    }
}
