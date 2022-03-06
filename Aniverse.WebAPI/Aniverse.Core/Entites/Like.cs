using Aniverse.Core.Entites.Enum;
using System;

namespace Aniverse.Core.Entites
{
    public class Like
    {
        public int Id { get; set; }
        public DateTime LikeDate { get; set; }
        public LikeType LikeType { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
