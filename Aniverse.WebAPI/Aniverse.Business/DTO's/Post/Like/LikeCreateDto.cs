using Aniverse.Core.Entites.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.DTO_s.Post.Like
{
    public class LikeCreateDto
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public int LikeType { get; set; }
        public bool IsLike { get; set; }
    }
}
