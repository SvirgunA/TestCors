using System.ComponentModel.DataAnnotations;
using TestCors.Common.Services;
using TestCors.DTO.Attributes;
using TestCors.Models;

namespace TestCors.DTO
{
    public class RegisterDto
    {
        [Required, EmailAddress, MaxLength(50)]
        public string Email { get; set; }

        [Required, MaxLength(30)]
        public string FirstName { get; set; }

        [Required, MaxLength(30)]
        public string LastName { get; set; }

        [Required, MaxLength(12)]
        public string PhoneNumber { get; set; }

        [Required, Password]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }

        public User ToEntity()
        {
            return new User
            {
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                PhoneNumber = this.PhoneNumber,
                PasswordHash = Password.Hash(),
                Verified = false
            };
        }
    }
}
