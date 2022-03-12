using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Core.Entites;
using Aniverse.Core.Entites.Enum;
using System.Collections.Generic;

namespace Aniverse.Business.DTO_s.User
{
    public class UserGetDto
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Gmail { get; set; }
        public string Birthday { get; set; }
        public string Bio { get; set; }
        public string ProfilPicture { get; set; }
        public string CoverPicture { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
    }
}
