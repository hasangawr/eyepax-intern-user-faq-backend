using Microsoft.AspNetCore.Mvc;
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
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var (isValid, principal) = _tokenServices.ValidateToken(token);
            if (token != null && isValid)
            {
                var users = await _service.GetAllReqResUsersAsync();
                return Ok(users);
            }
            return BadRequest();
            
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReqResUser>> GetInternalUser(Guid id)
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

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInternalUser(Guid id, PostUser postUser)
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult PostInternalUser(PostUser postUser)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var (isValid, principal) = _tokenServices.ValidateToken(token);
            if (token != null && isValid)
            {
                _service.CreateUserAsync(postUser);
                return Ok();
            }
            return BadRequest();
 
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInternalUser(Guid id)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var (isValid, principal) = _tokenServices.ValidateToken(token);
            if (token != null && isValid)
            {
                var internalUser = await _service.GetInternalUserAsync(id);
                if (internalUser == null)
                {
                    return NotFound();
                }

                _service.DeleteUserAsync(id);

                return NoContent();
            }
            return BadRequest();

        }
    }
}
