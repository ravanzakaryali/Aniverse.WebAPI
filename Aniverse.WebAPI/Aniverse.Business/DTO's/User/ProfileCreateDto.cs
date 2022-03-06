using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.DTO_s.User
{
    public class ProfileCreateDto
    {
        public string ImageName { get; set; }
        public string UserId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
