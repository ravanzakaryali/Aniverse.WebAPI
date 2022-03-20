using Aniverse.Business.DTO_s.Page;
using Aniverse.Business.DTO_s.Picture;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface IPageService
    {
        Task<PageGetDto> GetPageAsync(string pagename, HttpRequest request);
        Task<List<GetPictureDto>> GetPhotos(string pagename, HttpRequest request);
        Task<List<PageGetDto>> GetAllAsync(int page,int size,HttpRequest request);
        Task<List<PageGetDto>> GetFollowPages(int page,int size);
        Task PageCreateAsync(PageCreateDto createDto);
    }
}
