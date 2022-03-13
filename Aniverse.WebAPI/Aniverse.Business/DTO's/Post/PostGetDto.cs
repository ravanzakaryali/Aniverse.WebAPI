using Aniverse.Business.DTO_s.Animal;
using Aniverse.Business.DTO_s.Comment;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post.Like;
using Aniverse.Business.DTO_s.User;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Aniverse.Business.DTO_s.Post
{
    public class PostGetDto
    {
        public int Id { get; set; }
        public string Content { get;set; }
        public string HasTag { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsModified { get; set; }
        public string UserId { get; set; }
        public bool IsSave { get; set; } = false;
        public UserGetDto User { get; set; }
        public ICollection<GetPictureDto> Pictures { get; set; }
        public ICollection<LikeGetDto> Likes { get; set; }
        public ICollection<CommentGetDto> Comments { get; set; }
        public AnimalGetDto Animal { get; set; }
    }
}
