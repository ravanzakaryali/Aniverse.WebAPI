using Aniverse.Business.DTO_s.User;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.Http;
using System;

namespace Aniverse.Business.DTO_s.Story
{
    public class StoryGetDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get;set; }
        public string StoryFileName { get; set; }
        public string ImageSrc { get; set; }
        public UserAllDto User { get; set; }
    }
}
