using Aniverse.Core.Entites.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Aniverse.Core.Entites
{
    public class AppUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public string Bio { get; set; }
        public Gender Gender { get;set;}
        public DateTime Birthday { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsBlock { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceive { get; set; }
        public ICollection<UserFriend> Friends { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public UserSM UserSM { get; set; }
    }
}
