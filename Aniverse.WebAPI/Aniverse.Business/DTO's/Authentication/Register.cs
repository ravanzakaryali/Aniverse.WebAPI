using Aniverse.Core.Entites.Enum;
using System;

namespace Aniverse.Business.DTO_s.Authentication
{
    public class Register
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; } 
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Gender Gender { get; set; }
        
    } 
}
