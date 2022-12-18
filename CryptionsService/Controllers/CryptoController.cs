using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptionsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        // GET: api/<CryptoController>
        [HttpGet]
        public string Get()
        {
            return  "value";
           
        }

        // GET api/<CryptoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CryptoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CryptoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CryptoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
