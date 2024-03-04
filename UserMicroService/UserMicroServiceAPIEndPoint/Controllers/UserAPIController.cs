using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserBusinessLogicLayer;
using UserBusinessLogicLayer.TokenValidationServices;
using UserDataAccessLayer.Entities;

namespace UserMicroServiceAPIEndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserServices _service;
        private readonly ITokenServices _tokenServices;


        public UserAPIController(IUserServices service, ITokenServices tokenServices)
        {
            _service = service;
            _tokenServices= tokenServices;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReqResUser>>> GetReqResUsers()
        {
            try 
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var (isValid, principal) = _tokenServices.ValidateToken(token);
                if (token != null && isValid)
                {

                    var users = await _service.GetAllReqResUsersAsync();
                    return Ok(users);
                }
                return BadRequest();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500,ex.Message);
            }

            
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReqResUser>> GetInternalUser(Guid id)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var (isValid, principal) = _tokenServices.ValidateToken(token);
                if (token != null && isValid && principal != null)
                {
                    var userIdClaim = principal?.FindFirst("UserId")?.Value;
                    if (Guid.TryParse(userIdClaim, out var userId))
                    {
                        if (userId == id)
                        {
                            var internalUser = await _service.GetReqResUserAsync(id);

                            if (internalUser == null)
                            {
                                return NotFound();
                            }

                            return internalUser;
                        }
                        else
                        {
                            return Forbid();
                        }
                    }

                }
                return BadRequest();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex);
                return StatusCode(500,ex.Message);
            }


        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInternalUser(Guid id, PostUser postUser)
        {
            try {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var (isValid, principal) = _tokenServices.ValidateToken(token);
                if (token != null && isValid && principal != null)
                {
                    var userIdClaim = principal?.FindFirst("UserId")?.Value;
                    if (Guid.TryParse(userIdClaim, out var userId))
                    {
                        if (userId == id)
                        {
                            var internalUser = await _service.GetInternalUserAsync(id);
                            if (internalUser == null)
                            {
                                return BadRequest();
                            }

                            await _service.UpdateUserAsync(id, postUser);

                            return Ok();
                        }
                        else
                        {
                            return Forbid();
                        }

                    }
                }
                return BadRequest();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex);
                return StatusCode(500,ex.Message);
            }

        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostInternalUser(PostUser postUser)
        {
            try
            {

                InternalUser createdUser = await _service.CreateUserAsync(postUser);
                return StatusCode(200, new { msg = "Registration successful!", user = createdUser });


            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, new { msg = ex.Message });
            }
            catch (DbUpdateException ex)
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

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInternalUser(Guid id)
        {
            try 
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var (isValid, principal) = _tokenServices.ValidateToken(token);
                if (token != null && isValid && principal != null)
                {
                    var userIdClaim = principal?.FindFirst("UserId")?.Value;
                    if (Guid.TryParse(userIdClaim, out var userId))
                    {
                        if (userId == id)
                        {
                            var internalUser = await _service.GetInternalUserAsync(id);
                            if (internalUser == null)
                            {
                                return BadRequest();
                            }

                            _service.DeleteUserAsync(id); ;

                            return Ok("User Successfully Deleted");
                        }
                        else
                        {
                            return Forbid();
                        }

                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
