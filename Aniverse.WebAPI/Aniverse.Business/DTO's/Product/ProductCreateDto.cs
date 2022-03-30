using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Aniverse.Business.DTO_s.Product
{
    public class ProductCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Url { get; set; }
        public List<IFormFile> ImageFile { get; set; }
        public string UserId { get; set; }
        public int PageId { get; set; }
        public int CategoryId { get; set; }
        public ICollection<ProductImageDto> Pictures { get; set; }
    }
}
