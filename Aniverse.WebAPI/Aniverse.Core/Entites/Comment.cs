using System;
using System.Collections.Generic;

namespace Aniverse.Core.Entites
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SenderDate { get; set; }
        public bool IsModified { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int PostId { get; set; } 
        public Post Post { get; set; }
        public ICollection<Comment> ReplyComment { get; set; }
    }
}
