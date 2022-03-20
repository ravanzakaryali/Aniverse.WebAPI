using Aniverse.Core.Entites;
using System;
using System.Collections.Generic;

namespace Aniverse.Core.Entities
{
    public class Page
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Website { get; set; }
        public string BusinessName { get; set; }
        public bool IsOfficial { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Post> Posts { get; set; }


    }
}
