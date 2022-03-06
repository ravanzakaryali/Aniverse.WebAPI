using Aniverse.Business.DTO_s.User;
using System;

namespace Aniverse.Business.DTO_s.Animal
{
    public class AnimalAllDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Animalname { get; set; }
        public string ProfilePicture { get; set; } 
        public string UserId { get; set; }
        public UserAllDto User { get; set; }
    }
}
