using System.ComponentModel.DataAnnotations;
using TestCors.Data.Configurations;
using TestCors.Models;

namespace TestCors.DTO
{
    public class PhoneDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(CommonConfigurationValues.PhoneNameMaxLength)]
        public string Name { get; set; }

        public void Update(Phone entity)
        {
            entity.Name = this.Name;
        }

    }
}
