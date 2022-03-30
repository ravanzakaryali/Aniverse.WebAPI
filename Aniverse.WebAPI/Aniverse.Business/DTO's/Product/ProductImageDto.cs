using Microsoft.AspNetCore.Http;

namespace Aniverse.Business.DTO_s.Product
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string UserId { get; set; }
        public int? PageId { get; set; } = null;
        public IFormFile ImageFile { get; set; }
    }
}
