using Aniverse.Business.DTO_s.Friend;
using Aniverse.Business.DTO_s.Post;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface IPostService
    {
        Task<List<PostGetDto>> GetAllAsync(HttpRequest request);
        Task<List<PostGetDto>> GetAsync(string id, HttpRequest request);
        Task<List<PostGetDto>> GetFriendPost(ClaimsPrincipal user, HttpRequest request, int page, int size);
        Task CreateAsync(PostCreateDto postCreate, ClaimsPrincipal user);
    }
}
