using Aniverse.Business.DTO_s.Animal;
using Aniverse.Business.DTO_s.Comment;
using Aniverse.Business.DTO_s.Page;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post.Like;
using Aniverse.Business.DTO_s.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.DTO_s.Post
{
    public class PostPageGetDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsModified { get; set; }
        public string UserId { get; set; }
        public bool IsSave { get; set; } = false;
        public PageGetDto Page { get; set; }
        public ICollection<GetPictureDto> Pictures { get; set; }
        public ICollection<LikeGetDto> Likes { get; set; }
        public ICollection<CommentGetDto> Comments { get; set; }
    }
}
