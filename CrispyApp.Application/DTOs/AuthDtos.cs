using System;
using System.ComponentModel.DataAnnotations;

namespace CrispyApp.Application.DTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Debes ingresar tu usuario")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debes ingresar tu contraseña")]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class VerifyPasswordRequestDto
    {
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
