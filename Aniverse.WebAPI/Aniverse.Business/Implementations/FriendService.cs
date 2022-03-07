using Aniverse.Business.DTO_s.Friend;
using Aniverse.Business.DTO_s.User;
using Aniverse.Business.Exceptions;
using Aniverse.Business.Extensions;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using Aniverse.Core.Entites.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        public readonly IHttpContextAccessor _httpContextAccessor;
        public FriendService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ConfirmFriend(FriendConfirmDto friendDto)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var friend = await _unitOfWork.FriendRepository.GetAsync(u => u.UserId == userLoginId && u.FriendId == friendDto.FriendId);
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

        public async Task<List<UserGetDto>> GetAllAsync(string username)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var friends = await _unitOfWork.FriendRepository.GetAllAsync(u => u.User.UserName == username || u.Friend.UserName == username && u.Status == FriendRequestStatus.Accepted, "Friend");
            if(friends is null)
            {
                throw new NotFoundException("Friend is not found");
            }
            var friendIds = friends.Select(x => x.FriendId);
            var userIds = friends.Select(x => x.UserId);
            return _mapper.Map<List<UserGetDto>>(await _unitOfWork.UserRepository.GetAllAsync(u => u.UserName != username && friendIds.Contains(u.Id) || userIds.Contains(u.Id)));
        }

        public async Task<List<UserGetDto>> GetUserFriendRequestAsync()
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var frineds = await _unitOfWork.FriendRepository.GetAllAsync(u => u.UserId == userLoginId && u.Status == FriendRequestStatus.Pending, "Friend");
            if(frineds is null)
            {
                throw new NotFoundException("Friend is not found");
            }
            var friendsId = frineds.Select(f => f.FriendId);
            return _mapper.Map<List<UserGetDto>>(await _unitOfWork.UserRepository.GetAllAsync(u => friendsId.Contains(u.Id)));
        }
        public async Task AddFriendAsync(FriendRequestDto addFriend)
        {
            var userLoginId  = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == addFriend.Id);
            if (user is null)
            {
                throw new NotFoundException("User is not found");
            }
            var userfriends = await _unitOfWork.FriendRepository.GetAsync(f => f.UserId == user.Id && f.FriendId == userLoginId);
            if (userfriends != null)
            {
                throw new AlreadyException("Friendly or blocked user");
            }
            UserFriend userFriend = new UserFriend
            {
                UserId = addFriend.Id,
                FriendId = userLoginId,
                Status = FriendRequestStatus.Pending
            };
            await _unitOfWork.FriendRepository.CreateAsync(userFriend);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeleteFriendAsync(FriendRequestDto deleteFriend)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == deleteFriend.Id);
            if (user is null)
            {
                throw new NotFoundException("User is not found");
            }
            var userfriends = await _unitOfWork.FriendRepository.GetAsync(f => f.UserId == user.Id && f.FriendId == userLoginId);
            if (userfriends is null)
            {
                throw new AlreadyException("Friend is not found");
            }
            _unitOfWork.FriendRepository.Delete(userfriends);
            await _unitOfWork.SaveAsync();
        }
        public async Task FriendBlockAsync(FriendRequestDto friendBlock)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == friendBlock.Id);
            if (user is null)
            {
                throw new NotFoundException("User is not found");
            }
            var userfriends = await _unitOfWork.FriendRepository.GetAsync(f => f.UserId == user.Id && f.FriendId == userLoginId);
            if (userfriends is null)
            {
                throw new AlreadyException("Friend is not found");
            }
            userfriends.Status = FriendRequestStatus.Blocked;
            _unitOfWork.FriendRepository.Update(userfriends);
            await _unitOfWork.SaveAsync();
        }
        public async Task FriendUnBlockAsync(FriendRequestDto friendBlock)
        {
            var UserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == friendBlock.Id);
            if (user is null)
            {
                throw new NotFoundException("User is not found");
            }
            var userfriends = await _unitOfWork.FriendRepository.GetAsync(f => f.UserId == user.Id && f.FriendId == UserId);
            if (userfriends is null)
            {
                throw new AlreadyException("Friend is not found");
            }
            userfriends.Status = FriendRequestStatus.Declined;
            _unitOfWork.FriendRepository.Update(userfriends);
            await _unitOfWork.SaveAsync();
        }

    }
}
