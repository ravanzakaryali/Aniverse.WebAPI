using Aniverse.Core.Entities;
using System;
using System.Collections.Generic;

namespace Aniverse.Core.Entites
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Price { get; set; }
        public string Url { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int PageId { get; set; }
        public Page Page { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public ICollection<Picture> Pictures { get; set; }
    }
}
