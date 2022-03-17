using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.DTO_s.Animal
{
    public class AnimalPictureChangeDto
    {
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
    }
}
