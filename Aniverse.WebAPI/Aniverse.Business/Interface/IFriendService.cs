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
        Task<List<UserGetDto>> GetAllAsync(string username, HttpRequest request);
        Task<List<UserGetDto>> GetUserFriendRequestAsync();
        Task ConfirmFriend(FriendConfirmDto friend);
        Task AddFriendAsync(FriendRequestDto addFriend);
        Task DeleteFriendAsync(FriendRequestDto friend);
        Task FriendBlockAsync(FriendRequestDto friend);
        Task FriendUnBlockAsync(FriendRequestDto friend);
    }
}
