using Aniverse.Business.DTO_s.Story;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface IStoryService
    {
        Task<List<StoryGetDto>> GetAllAsync();
        Task<List<StoryGetDto>> GetUserAsync(string username, HttpRequest request);
        Task CreateAsync(StoryCreateDto storyCreate, ClaimsPrincipal user);
        Task<List<StoryGetDto>> GetFriendAsync(string username, HttpRequest request);
    }
}
