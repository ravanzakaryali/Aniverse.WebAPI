using System.Collections.Generic;

namespace Aniverse.Core.Entites
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<AnimalGroup> Animals { get; set; }
    }
}
