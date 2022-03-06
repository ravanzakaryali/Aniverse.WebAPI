using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.DTO_s.User;
using System.Collections.Generic;

namespace Aniverse.Business.DTO_s.Friend
{
    public class UserFriendDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserGetDto Friend { get; set; }

    }
}
