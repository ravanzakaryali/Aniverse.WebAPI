using System;
using System.Collections.Generic;

namespace Aniverse.Core.Entites
{
    public class Product
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Hastag { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public ICollection<Picture> Pictures { get; set; }
    }
}
