using System.ComponentModel.DataAnnotations;

namespace IdentityService.DTOs.inputs
{
    public class AuthUserInputDto
    {
        [Required]
        public string Cellphone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Fname { get; set; }

        [Required]
        public string Lname { get; set; }
    }
}