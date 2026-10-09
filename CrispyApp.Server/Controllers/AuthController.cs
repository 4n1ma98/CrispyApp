using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CrispyApp.Application.DTOs;
using CrispyApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CrispyApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public AuthController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var user = _userRepo.GetByUsername(request.Username);
            if (user == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            var hash = _userRepo.HashPassword(request.Password);
            if (user.PasswordHash != hash)
            {
                return Unauthorized("Contraseña incorrecta.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ThisIsASecretKeyForCrispyApp12345!@#");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new LoginResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                Username = user.Username,
                Role = user.Role
            });
        }

        [HttpPost("verify-password")]
        public IActionResult VerifyPassword([FromBody] VerifyPasswordRequestDto request)
        {
            // Validar la contraseña contra el usuario actual "admin" (para el desbloqueo)
            var user = _userRepo.GetByUsername("admin");
            if (user == null) return Unauthorized();

            var hash = _userRepo.HashPassword(request.Password);
            if (user.PasswordHash == hash)
            {
                return Ok(new { success = true });
            }
            
            return Unauthorized(new { success = false, message = "Contraseña incorrecta" });
        }
    }
}
