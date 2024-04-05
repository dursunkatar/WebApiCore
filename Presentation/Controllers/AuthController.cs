using Application.Commands;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] UserLoginCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Success)
            {
                var token = GenerateJwtToken(command.Username);
                response.Data.AccessToken = token;
                return Ok(response);

            }
            return Unauthorized(response);
        }

        [HttpPost("refreshtokenlogin")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Success)
            {
                var token = GenerateJwtToken(response.Data.UserName);
                response.Data.AccessToken = token;
                return Ok(response);

            }
            return Unauthorized(response);
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
    };

            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(2), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
