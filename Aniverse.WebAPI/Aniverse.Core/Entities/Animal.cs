using System;
using System.Collections.Generic;

namespace Aniverse.Core.Entites
{
    public class Animal
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Animalname { get; set; }
        public DateTime Birthday { get; set; }
        public string Breed { get; set; }
        public string Bio { get; set; }
        public int AnimalCategoryId { get; set; }
        public AnimalCategory AnimalCategory { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public AnimalFollow AnimalFollow { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<AnimalGroup> Groups { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
