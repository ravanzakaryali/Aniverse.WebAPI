using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.DTO_s.Animal
{
    public class FollowDto
    {
        public int AnimalId { get; set; }
        public bool IsFollowing { get; set; }
        public string UserId { get; set; }

    }
}
