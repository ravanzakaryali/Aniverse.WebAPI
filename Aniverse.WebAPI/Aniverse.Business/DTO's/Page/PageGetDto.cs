using Aniverse.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.DTO_s.Page
{
    public class PageGetDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Website { get; set; }
        public string Category { get; set; }
        public string Pagename { get; set; }
        public bool IsOfficial { get; set; }
        public List<PageFollow> PageFollow { get; set; }
        public string ProfilPicture { get; set; }
        public string CoverPicture { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
