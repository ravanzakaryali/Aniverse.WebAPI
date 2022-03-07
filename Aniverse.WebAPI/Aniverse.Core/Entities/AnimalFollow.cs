using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Core.Entites
{
    public class AnimalFollow
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public DateTime FollowDate { get; set; }
    }
}
