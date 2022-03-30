using Aniverse.Core.Entites;
using System;

namespace Aniverse.Core.Entities
{
    public class SaveProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } 
        public string  UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime SaveAddDate { get; set; }

    }
}
