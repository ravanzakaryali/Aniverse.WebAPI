using Microsoft.AspNetCore.Http;
using System;

namespace Aniverse.Core.Entites
{
    public class Story
    {
        public int Id { get; set; }
        public string StoryFileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; } 
        public string ImageSrc { get; set; }
    }
}
