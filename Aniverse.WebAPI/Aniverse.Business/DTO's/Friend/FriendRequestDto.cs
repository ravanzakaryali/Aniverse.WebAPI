using Aniverse.Business.DTO_s.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.DTO_s.Friend
{
    public class FriendRequestDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public UserGetDto Sender { get; set; }
        public string ReceiverId { get; set; }
        public UserGetDto Receiver { get; set; }
        public string Status { get; set; }
    }
}
