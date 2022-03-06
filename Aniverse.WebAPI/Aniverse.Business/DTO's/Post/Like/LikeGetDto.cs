using Aniverse.Core.Entites.Enum;
using System;

namespace Aniverse.Business.DTO_s.Post.Like
{
    public class LikeGetDto
    {
        public int Id { get; set; }
        public DateTime LikeDate { get; set; }
        public LikeType LikeType { get; set; }
        public string UserId { get; set; } 
        public int PostId { get; set; }
    }
}
