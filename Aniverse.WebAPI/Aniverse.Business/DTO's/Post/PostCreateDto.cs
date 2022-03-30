using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Aniverse.Business.DTO_s.Post
{
    public class PostCreateDto
    {   
        public string Content { get; set; }
        public string UserId { get; set; }
        public int? PageId { get; set; } = null;
        public int? AnimalId { get; set; } = null;
        public List<IFormFile> ImageFile { get; set; }
        public ICollection<PostImageDto> Pictures { get; set; }
    }
}
