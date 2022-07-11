using ContactsService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactsService.Services
{
    public class MockApiAuth : IApiAuth
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<MockApiAuth> _logger;

        public MockApiAuth(IConfiguration configuration, ILogger<MockApiAuth> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public string AuthenticateUser(string username, string password)
        {
            if (username == _configuration["User:Username"] && password == _configuration["User:Password"])
            {
                _logger.LogInformation($"User: { _configuration["User:Username"]} logged in at " + DateTime.Now);
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:IssuerSigningKey"]));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _configuration["User:Username"] ),
                    new Claim(ClaimTypes.Role, _configuration["User:Role"])
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signingCredentials
                    );

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return token;
            }

            return "";
        }
    }
}
