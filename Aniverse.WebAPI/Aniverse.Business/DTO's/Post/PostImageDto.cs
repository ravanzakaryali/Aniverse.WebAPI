using Microsoft.AspNetCore.Http;

namespace Aniverse.Business.DTO_s.Post
{
    public class PostImageDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int? PostId { get; set; } = null;
        public string UserId { get; set; }
        public int? AnimalId { get; set; } = null;
        public int? PageId { get; set; } = null;
        public IFormFile ImageFile { get; set; }
    }
}
