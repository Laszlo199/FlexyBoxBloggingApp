using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Security
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email can not be empty!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can not be empty!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username can not be empty!")]
        public string Username { get; set; }
    }
}
