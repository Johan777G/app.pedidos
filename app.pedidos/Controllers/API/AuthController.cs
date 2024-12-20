using app.pedidos.Utilidades.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace app.pedidos.Controllers.API
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            if (dto.Usuario == "admin" && dto.Contrasena == "1234")
            {
                var token = GenerateToken(dto.Usuario, "Admin");
                return Ok(new { Token = token });
            }
            else if (dto.Usuario == "cliente" && dto.Contrasena == "1234")
            {
                var token = GenerateToken(dto.Usuario, "Cliente");
                return Ok(new { Token = token });
            }

            return Ok("Error al obtener usuario.");
        }


        public string GenerateToken(string usuario, string rol)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, rol),
                new Claim(JwtRegisteredClaimNames.Name, usuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                        ),
                    SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
