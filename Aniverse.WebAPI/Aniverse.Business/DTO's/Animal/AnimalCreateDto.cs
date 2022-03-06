using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.DTO_s.Animal
{
    public class AnimalCreateDto
    {
        public string Name { get; set; }
        public string Animalname { get; set; }
        public DateTime Birthday { get; set; }
        public string Breed { get; set; }
        public int AnimalCategoryId { get; set; }
        public string UserId { get; set; }
    }
}
