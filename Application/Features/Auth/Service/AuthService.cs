using Application.Features.Auth.DTOs;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Features.Auth.Service
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, ILogger<AuthService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());

            if (existingUser != null)
            {
                _logger.LogWarning("Registration rejected: email {Email} is already registered.", dto.Email);
                return false;
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName= dto.Fullname,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role= "Customer",
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("New user registered: {UserId} ({Email}).", user.Id, user.Email);
            return true;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            _logger.LogInformation("Login attempt for {Email}.", dto.Email);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());

            if (user == null)
            {
                _logger.LogWarning("Login failed for {Email}: no such user.", dto.Email);
                return null;
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isValidPassword)
            {
                _logger.LogWarning("Login failed for {UserId}: invalid password.", user.Id);
                return null;
            }

            var token = GenerateJwtToken(user);
            _logger.LogInformation("Login succeeded for {UserId}.", user.Id);

            return new LoginResponseDto
            {
                Id = user.Id,
                Token = token,
                Fullname = user.FullName,
                Email = user.Email
            };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? "SuperSecretKeyForAuthPartSystemTesting12345!";
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? "AuthPartIssuer";
            var jwtAudience = _configuration["Jwt:Audience"] ?? "AuthPartAudience";
            var expiresMinutes = double.TryParse(_configuration["Jwt:ExpiresInMinutes"], out var exp) ? exp : 60;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
