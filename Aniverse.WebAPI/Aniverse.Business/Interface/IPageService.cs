using Aniverse.Business.DTO_s.Page;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.DTO_s.User;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface IPageService
    {
        Task<PageGetDto> GetPageAsync(string pagename, HttpRequest request);
        Task<List<GetPictureDto>> GetPhotos(string pagename,int page, int size, HttpRequest request);
        Task<List<PageGetDto>> GetAllAsync(int page,int size,HttpRequest request);
        Task<List<PageGetDto>> GetFollowPages(int page,int size);
        Task PageCreateAsync(PageCreateDto createDto);
        Task<List<PostPageGetDto>> GetPostsAsync(int page, int size,string pagename, HttpRequest request);
        Task ProfileCreate(int id,ProfileCreateDto profileCreate);
        Task CoverCreate(int id,ProfileCreateDto profileCreate);
        Task<List<UserGetDto>> GetPageFollowersUser(int id,int page,int size, HttpRequest request);
        Task<List<PageGetDto>> GetUserFollowPages(string id, HttpRequest request);
        Task PageUpdate(int id, PageUpdateDto pageUpdate);
        Task PageFollow(int id);
        Task PageUnfollow(int id);
    }
}
