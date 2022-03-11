using Aniverse.Core.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Core.Entities
{
    public class SavePost
    {
        public string Id { get; set; }
        public string CreationDate { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }

    }
}
