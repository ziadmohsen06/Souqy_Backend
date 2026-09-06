using System.ComponentModel.DataAnnotations;

namespace Application.Features.Auth.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Fullname { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
