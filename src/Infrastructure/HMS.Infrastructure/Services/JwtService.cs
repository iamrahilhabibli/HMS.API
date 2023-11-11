using HMS.Application.Abstraction.Services;
using HMS.Application.DTOs.Response_DTOs;
using HMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HMS.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public JwtService(IConfiguration configuration,
                          UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<TokenResponseDto> CreateJwt(AppUser user)
        {
            DateTime jwtExpiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));
            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Subject to UserID
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // new JWT Id
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), // Token generated Date Time
                new Claim(ClaimTypes.NameIdentifier, user.Email.ToString()), // Unique name identifier of the user(email)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: jwtExpiration,
                signingCredentials: signingCredentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(tokenGenerator);
            string refreshToken = GenerateRefreshToken();

            DateTime refreshTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_MINUTERS"]));
            return new TokenResponseDto(token, jwtExpiration,refreshToken, refreshTokenExpiration);
        }
        public string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];
            var randomNumber = RandomNumberGenerator.Create();
            randomNumber.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
