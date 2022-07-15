using System;

namespace IdentityService.DTOs.outputs
{
    public class AuthUserOutputDto
    {
        public Guid Id { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
    }
}