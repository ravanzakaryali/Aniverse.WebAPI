using Aniverse.Business.DTO_s.User;
using Aniverse.Core.Entites;
using System;
using System.Collections.Generic;

namespace Aniverse.Business.DTO_s.Animal
{
    public class AnimalGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Animalname { get; set; }
        public DateTime Birthday { get; set; }
        public string Breed { get; set; }
        public int AnimalCategoryId { get; set; }
        public string UserId { get; set; }
        public string Bio { get;set; }
        public UserGetDto User { get; set; }
        public string ProfilPicture { get; set; }
        public string CoverPicture { get; set; }
        public int PostCount { get; set; }
        public ICollection<AnimalFollowDto> AnimalFollow { get; set; }
        public AnimalCategory AnimalCategory { get; set; }
    }
}
