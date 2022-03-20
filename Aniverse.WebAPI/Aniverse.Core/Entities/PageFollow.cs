using Aniverse.Core.Entites;
using System;

namespace Aniverse.Core.Entities
{
    public class PageFollow
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public Page Page { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime FollowDate { get; set; }
    }
}
