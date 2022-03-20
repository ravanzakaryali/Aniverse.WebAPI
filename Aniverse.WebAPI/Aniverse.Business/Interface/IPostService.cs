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
        Task<List<PostGetDto>> GetAllAsync(HttpRequest request, int page, int size);
        Task<List<PostGetDto>> GetAsync(string id, HttpRequest request);
        Task<List<PostGetDto>> GetFriendPost(HttpRequest request, int page, int size);
        Task<List<PostGetDto>> GetAllArchive(HttpRequest request,int page,int size);
        Task<List<PostGetDto>> GetAllRecycle(HttpRequest request,int page,int size);
        Task<List<PostGetDto>> GetAllSave(HttpRequest request,int page,int size);
        Task CreateAsync(PostCreateDto postCreate);
        Task PostSaveAsync(PostSaveDto postSave);
        Task PostUpdateAsync(int id, PostCreateDto postCreate);
        Task<List<PostGetDto>> GetAnimalPosts(string animalname, HttpRequest request);
        Task PostDeleteAsync(int id);
        Task PostArchiveAsync(int id);
        Task PostDbDeleteAsync(int id);
    }
}
