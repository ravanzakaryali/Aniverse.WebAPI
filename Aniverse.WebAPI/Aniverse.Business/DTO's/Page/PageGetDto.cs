using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.DTO_s.Page
{
    public class PageGetDto
    {
        public string Name { get; set; }
        public string About { get; set; }
        public string Website { get; set; }
        public string Category { get; set; }
        public string Pagename { get; set; }
        public bool IsOfficial { get; set; }
        public int FollowCount { get; set; }
        public string ProfilePicture { get; set; }
        public string CoverPicture { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
