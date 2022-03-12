using Microsoft.AspNetCore.Http;

namespace Aniverse.Business.DTO_s.Post
{
    public class PostImageDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int? PostId { get; set; }
        public string UserId { get; set; }
        public int? AnimalId { get; set; } 
        public IFormFile ImageFile { get; set; }
    }
}
