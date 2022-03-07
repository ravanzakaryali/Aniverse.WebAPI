using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Core.Entites
{
    public class AnimalGroup
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
