using Aniverse.Business.DTO_s.User;
using Aniverse.Core.Entites;
using System;
using System.Collections.Generic;

namespace Aniverse.Business.DTO_s.Comment
{
    public class CommentGetDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SenderDate { get; set; }
        public bool IsModified { get; set; }
        public ICollection<CommentGetDto> ReplyComment { get; set; }
        public UserGetDto User { get; set; }
    }
}
