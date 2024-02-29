using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Runtime.CompilerServices;

namespace RegistrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRegistrationService registrationService;
        public UsersController(IRegistrationService registrationService) { 
        this.registrationService = registrationService;
        }

        // post a user
        [HttpPost]
        public async Task<IActionResult> RegisterUser(AddUserReq addUserReq)
        {
            try
            {
                           
                User createdUser = await registrationService.AddUser(addUserReq);
                return StatusCode(200, new { msg = "Registration successful!" , user = createdUser});


            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, new {msg = ex.Message});
            }
            catch(DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { msg = "Database error!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { msg = "Error regestering user!" });
               
            }

            
        }
    }
}
