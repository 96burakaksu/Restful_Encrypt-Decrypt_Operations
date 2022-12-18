using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AuthService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private IJWTAuthenticationManager _jWTAuthenticationManager;
       


        public AuthController(IJWTAuthenticationManager jWTAuthenticationManager)
        {
            _jWTAuthenticationManager = jWTAuthenticationManager;
          
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
        public IActionResult Validation()
        {
            return Ok();
        }


     
    }
  



}
