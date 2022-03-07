using Aniverse.Business.DTO_s.Friend;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.DTO_s.User;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface IFriendService
    {
        Task<List<UserGetDto>> GetAllAsync(string username, ClaimsPrincipal user);
        Task<List<UserGetDto>> GetUserFriendRequestAsync(ClaimsPrincipal user);
        Task ConfirmFriend(ClaimsPrincipal user, FriendConfirmDto friend);
        Task AddFriendAsync(FriendRequestDto addFriend,ClaimsPrincipal user);
        Task DeleteFriendAsync(FriendRequestDto friend, ClaimsPrincipal user);
    }
}
