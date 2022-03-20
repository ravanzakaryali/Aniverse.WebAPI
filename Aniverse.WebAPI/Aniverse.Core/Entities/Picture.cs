using Aniverse.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Aniverse.Core.Entites
{
    public class Picture
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsProfilePicture { get; set; }
        public bool IsCoverPicture { get; set; }
        public bool IsAnimalProfilePicture { get; set; }
        public bool IsAnimalCoverPicture { get; set; }
        public bool IsPageProfilePicture { get; set; }
        public bool IsPageCoverPicture { get; set; }
        public int? AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int? PostId { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int? PageId { get; set; }
        public Page Page { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
