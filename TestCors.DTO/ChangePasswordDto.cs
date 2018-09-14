using System.ComponentModel.DataAnnotations;
using TestCors.DTO.Attributes;

namespace TestCors.DTO
{
    public class ChangePasswordDto
    {
        [Required, Password]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
