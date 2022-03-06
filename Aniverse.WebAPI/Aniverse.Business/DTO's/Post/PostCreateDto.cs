using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Aniverse.Business.DTO_s.Post
{
    public class PostCreateDto
    {   
        public string Content { get; set; }
        public string HasTag { get; set; }
        public string UserId { get; set; }
        public int AnimalId { get; set; }
        public List<IFormFile> ImageFile { get; set; }
        public ICollection<PostImageDto> Pictures { get; set; }
    }
}
