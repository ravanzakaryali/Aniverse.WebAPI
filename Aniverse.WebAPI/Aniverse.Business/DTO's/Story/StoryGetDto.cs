using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.User;
using System;

namespace Aniverse.Business.DTO_s.Story
{
    public class StoryGetDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get;set; }
        public string StoryFileName { get; set; }
        public string Content { get; set; }
        public GetPictureDto Pictures { get; set; }
        public string ImageSrc { get; set; }
        public UserGetDto User { get; set; }
    }
}
