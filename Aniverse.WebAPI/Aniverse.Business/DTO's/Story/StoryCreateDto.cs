using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.DTO_s.Story
{
    public class StoryCreateDto  
    {
        public string StoryFileName { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; } 
        public IFormFile StoryFile { get; set; }
    }
}
