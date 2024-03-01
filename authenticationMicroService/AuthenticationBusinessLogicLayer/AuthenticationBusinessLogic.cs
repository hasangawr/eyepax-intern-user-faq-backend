using AuthenticationBusinessLogicLayer.PasswordServices;
using AuthenticationDataAccessLayer.AuthenticationRepo;
using AuthenticationDataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationBusinessLogicLayer
{
    public class AuthenticationBusinessLogic : IAuthenticationBusinessLogic
    {
        private readonly IAuthenticationRepo AuthRepo;
        private readonly IConfiguration configuration;
        private readonly IPasswordHasher _pwServices;
        public AuthenticationBusinessLogic(IAuthenticationRepo AuthRepo, IConfiguration configuration, IPasswordHasher pwServices)
        {
            this.AuthRepo = AuthRepo;
            this.configuration = configuration;
            this._pwServices = pwServices;
        }

        public async Task<string> Authenticate(ReqUser user)
        {
            var valid = await AuthRepo.GetUserAsync(user.UserName);
            if (valid != null && user != null && _pwServices.Verify(valid.Password, user.Password))
            {
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:SecretKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", valid.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName)
                      
                    }),
                    Expires = DateTime.UtcNow.AddHours(0.5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return tokenString;
            }
            return null;
        }


    }
}
