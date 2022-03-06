using System;
using System.Collections.Generic;

namespace Aniverse.Core.Entites
{
    public class AnimalCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Animal> Animals { get; set; }
    }
}
