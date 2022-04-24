using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.User;
using System;
using System.Collections.Generic;

namespace Aniverse.Business.DTO_s.Product
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSave { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public UserGetDto User { get; set; }
        public int CategoryId { get; set; }
        public ICollection<GetPictureDto> Pictures { get; set; }
    }
}
