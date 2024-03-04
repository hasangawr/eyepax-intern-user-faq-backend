using AuthenticationBusinessLogicLayer;
using AuthenticationDataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;


namespace authenticationAPIEndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationBusinessLogic _service;


        public AuthenticationController(IAuthenticationBusinessLogic service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] ReqUser user)
        {
            try 
            {
                // Check if the model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request body");
                }

                // Check authentication
                var isAuthenticated = await _service.Authenticate(user);

                if (isAuthenticated == null)
                {
                    return Unauthorized();
                }

                // Return the JWT token
                return Ok(new { Token = isAuthenticated });
            } 
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return StatusCode(500,ex.Message);  
            }

        }
    }

 
}
