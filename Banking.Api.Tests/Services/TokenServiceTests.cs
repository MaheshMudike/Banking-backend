using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Banking.Api.Domain;
using Banking.Api.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Banking.Api.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly TokenService _service;
        private readonly IConfiguration _config;

        public TokenServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                { "Jwt:Key", "THIS_IS_A_TEST_SECRET_KEY_123456789" },
                { "Jwt:Issuer", "TestIssuer" },
                { "Jwt:Audience", "TestAudience" },
                { "Jwt:ExpiresInMinutes", "30" }
            };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _service = new TokenService(_config);
        }

        [Fact]
        public void GenerateToken_ShouldReturn_NonEmptyString()
        {
            var user = new User { Id = 1, Username = "mahesh" };

            var token = _service.GenerateToken(user);

            Assert.False(string.IsNullOrWhiteSpace(token));
        }

        [Fact]
        public void GenerateToken_ShouldContainCorrectClaims()
        {
            var user = new User { Id = 10, Username = "mahesh" };

            var token = _service.GenerateToken(user);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            Assert.Equal("10", jwt.Claims.First(c => c.Type == "userId").Value);
            Assert.Equal("mahesh", jwt.Claims.First(c => c.Type == "username").Value);
        }

        [Fact]
        public void GenerateToken_ShouldHaveCorrectExpiration()
        {
            var user = new User { Id = 1, Username = "mahesh" };

            var token = _service.GenerateToken(user);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var expectedExpiry = DateTime.UtcNow.AddMinutes(30);

            Assert.True(jwt.ValidTo > DateTime.UtcNow);
            Assert.True(jwt.ValidTo <= expectedExpiry.AddSeconds(5)); // small buffer
        }

        [Fact]
        public void GenerateToken_ShouldBeValidSignature()
        {
            var user = new User { Id = 1, Username = "mahesh" };

            var token = _service.GenerateToken(user);

            var handler = new JwtSecurityTokenHandler();

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
                )
            };

            handler.ValidateToken(token, validationParams, out var validatedToken);

            Assert.NotNull(validatedToken);
            Assert.IsType<JwtSecurityToken>(validatedToken);
        }
    }
}
