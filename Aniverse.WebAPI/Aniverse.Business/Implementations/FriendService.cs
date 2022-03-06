using Aniverse.Business.DTO_s.Friend;
using Aniverse.Business.DTO_s.User;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using Aniverse.Core.Entites.Enum;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Implementations
{
    public class FriendService : IFriendService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public FriendService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ConfirmFriend(ClaimsPrincipal user, FriendConfirmDto friendDto)
        {
            var id = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            var friend = await _unitOfWork.FriendRepository.GetAsync(u => u.UserId == id && u.FriendId == friendDto.FriendId);
            if (friendDto.IsConfirm)
            {
                friend.Status = FriendRequestStatus.Accepted;
            }
            else
            {
                friend.Status = FriendRequestStatus.Declined;
            }
            _unitOfWork.FriendRepository.Update(friend);

            await _unitOfWork.SaveAsync();
        }

        public async Task<List<UserGetDto>> GetAllAsync(ClaimsPrincipal user)
        {
            var id = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            var frineds = await _unitOfWork.FriendRepository.GetAllAsync(u => u.UserId == id && u.Status == FriendRequestStatus.Accepted, "Friend");
            var friendsId = frineds.Select(f=>f.FriendId);
            return _mapper.Map<List<UserGetDto>>(await _unitOfWork.UserRepository.GetAllAsync(u=>friendsId.Contains(u.Id)));
        }


        public async Task<List<UserGetDto>> GetUserFriendRequestAsync(ClaimsPrincipal user)
        {
            var id = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            var frineds = await _unitOfWork.FriendRepository.GetAllAsync(u => u.UserId == id && u.Status == FriendRequestStatus.Pending, "Friend");
            var friendsId = frineds.Select(f => f.FriendId);
            return _mapper.Map<List<UserGetDto>>(await _unitOfWork.UserRepository.GetAllAsync(u => friendsId.Contains(u.Id)));
        }

    }
}
