using Aniverse.Business.DTO_s.Friend;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.DTO_s.User;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface IFriendService
    {
        Task<List<UserGetDto>> GetAllAsync(string username, HttpRequest request, int page,int size);
        Task<List<UserGetDto>> GetUserFriendRequestAsync(int page,int size,HttpRequest request);
        Task ConfirmFriend(string id);
        Task AddFriendAsync(string id);
        Task DeleteFriendAsync(string id);
        Task FriendBlockAsync(string id);
        Task FriendUnBlockAsync(string id);
        Task Declined(string id);
    }
}
