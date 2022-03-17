using System;
using System.Collections.Generic;

namespace Aniverse.Core.Entites
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Hastag { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsModified { get; set; }
        public bool IsArchive { get; set; }
        public bool IsDelete { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int? AnimalId { get; set; }
        public Animal Animal { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Picture> Pictures { get; set; }
    }
}
