using Application.Features.Auth.DTOs;
using Application.Features.Auth.Service;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace Souqy.Tests
{
    public class AuthTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        private IConfiguration GetConfiguration()
        {
            var inMemorySettings = new Dictionary<string, string?>
            {
                {"Jwt:Key", "SuperSecretKeyForAuthSystemTesting12345!"},
                {"Jwt:Issuer", "SouqyAuthIssuer"},
                {"Jwt:Audience", "SouqyAuthAudience"},
                {"Jwt:ExpiresInMinutes", "60"}
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        [Fact]
        public async Task RegisterAsync_ValidUser_ReturnsTrueAndCreatesUser()
        {
            // Arrange
            var context = GetDbContext();
            var config = GetConfiguration();
            var service = new AuthService(context, config, NullLogger<AuthService>.Instance);

            var dto = new RegisterDto
            {
                Fullname = "Test User",
                Email = "test@example.com",
                Password = "Password123!"
            };

            // Act
            var result = await service.RegisterAsync(dto);

            // Assert
            Assert.True(result);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == "test@example.com");
            Assert.NotNull(user);
            Assert.Equal("Test User", user.FullName);
            Assert.Equal("Customer", user.Role);
            Assert.True(BCrypt.Net.BCrypt.Verify("Password123!", user.PasswordHash));
        }

        [Fact]
        public async Task RegisterAsync_DuplicateEmail_ReturnsFalse()
        {
            // Arrange
            var context = GetDbContext();
            var config = GetConfiguration();
            var service = new AuthService(context, config, NullLogger<AuthService>.Instance);

            var dto = new RegisterDto
            {
                Fullname = "User One",
                Email = "duplicate@example.com",
                Password = "Password123!"
            };

            await service.RegisterAsync(dto);

            // Act
            var result = await service.RegisterAsync(new RegisterDto
            {
                Fullname = "User Two",
                Email = "DUPLICATE@example.com", // Case-insensitive check
                Password = "Password456!"
            });

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsTokenAndUserInfo()
        {
            // Arrange
            var context = GetDbContext();
            var config = GetConfiguration();
            var service = new AuthService(context, config, NullLogger<AuthService>.Instance);

            await service.RegisterAsync(new RegisterDto
            {
                Fullname = "Jane Doe",
                Email = "jane@example.com",
                Password = "SecurePassword123!"
            });

            // Act
            var response = await service.LoginAsync(new LoginDto
            {
                Email = "jane@example.com",
                Password = "SecurePassword123!"
            });

            // Assert
            Assert.NotNull(response);
            Assert.False(string.IsNullOrWhiteSpace(response.Token));
            Assert.Equal("Jane Doe", response.Fullname);
            Assert.Equal("jane@example.com", response.Email);
        }

        [Fact]
        public async Task LoginAsync_InvalidPassword_ReturnsNull()
        {
            // Arrange
            var context = GetDbContext();
            var config = GetConfiguration();
            var service = new AuthService(context, config, NullLogger<AuthService>.Instance);

            await service.RegisterAsync(new RegisterDto
            {
                Fullname = "John Doe",
                Email = "john@example.com",
                Password = "CorrectPassword123!"
            });

            // Act
            var response = await service.LoginAsync(new LoginDto
            {
                Email = "john@example.com",
                Password = "WrongPassword!"
            });

            // Assert
            Assert.Null(response);
        }
    }
}
