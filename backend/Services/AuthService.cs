using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; 
using System.Text;
using bioinsumos_asproc_backend.Models;
using  bioinsumos_asproc_backend.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Enyim.Caching;

namespace bioinsumos_asproc_backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly BioinsumosContext _dbcontext;
        private readonly IMemcachedClient _memcachedClient;
        private readonly uint _sessionTimeOutMinutes;

        public AuthService(IConfiguration config, BioinsumosContext dbcontext, IMemcachedClient memcachedClient)
        {
            _config = config;
            _dbcontext = dbcontext;
            _memcachedClient = memcachedClient;
            _sessionTimeOutMinutes = _config.GetValue<uint>("SessionParams:TimeoutMinutes");
        }

        public async Task<User> GetUser(string Email, string Password)
        {
            return await _dbcontext.Users.Where(u => 
                u.Email == Email &&
                u.Password == Password &&
                u.Status == true)
            .FirstOrDefaultAsync();
        }

        public async Task<SigninDtoResponse> SaveUser(AuthDtoRequest authDtoRequest)
        {
            try
            {
                User user = new()
                {
                    UserId = 0,
                    Email = authDtoRequest.Email,
                    Password = authDtoRequest.Password,
                };
                _dbcontext.Users.Add(user);
                await _dbcontext.SaveChangesAsync();

                return new()
                {
                    UserId = user.UserId,
                    Result = true,
                    Message = "User created"
                };
            }
            catch(Exception ex)
            {
                return new()
                {
                    UserId = 0,
                    Result = false,
                    Message = ex.Message
                }; 
            }
        }

        private string GenToken(string userId)
        {
            var key = _config.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));

            var credentialsToken = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(_sessionTimeOutMinutes),
                SigningCredentials = credentialsToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);


            string createdToken = tokenHandler.WriteToken(tokenConfig);

            return createdToken;
        }

        public async Task<AuthDtoResponse> GetToken(AuthDtoRequest authDtoRequest)
        {
            var user = await GetUser(authDtoRequest.Email, authDtoRequest.Password);
            if (user == null) {
                return await Task.FromResult<AuthDtoResponse>(null);
            }

            string createdToken = GenToken(user.UserId.ToString());

            return new AuthDtoResponse
                {
                    Token = createdToken,
                    Result = true,
                    Message = "User authenticated"
                };
        }
   
        public async Task<AuthDtoResponse> DeleteToken(string token)
        {
            var res = new AuthDtoResponse()
            {
                Token = "",
                Result = true,
                Message = ""
            };

            try 
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
                var expiration = jwtToken.ValidTo;
                var timeToLive = expiration - DateTime.UtcNow;

                await _memcachedClient.SetAsync(token, true, timeToLive);

                res.Message = "Logout successful. Token invalidated.";
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Result = false;
                return res;
            }
        }
   }
}