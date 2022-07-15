using System;

namespace UserService.DTOs
{
    public class UserOutputDto
    {
        public Guid Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
    }
}