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
using System;
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

       

        public async Task<List<UserGetDto>> GetAllAsync(string username, HttpRequest request, int page=1, int size=4)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var friends = await _unitOfWork.FriendRepository.GetAllPaginateAsync(page,size,u=>u.SenderDate,u => (u.User.UserName == username || u.Friend.UserName == username) && u.Status == FriendRequestStatus.Accepted);
            if(friends is null)
            {
                throw new NotFoundException("Friend is not found");
            }
            var friendIds = friends.Select(x => x.FriendId);
            var userIds = friends.Select(x => x.UserId);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => userIds.Contains(p.UserId) || friendIds.Contains(p.UserId));
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            var friendsMap = _mapper.Map<List<UserGetDto>>(await _unitOfWork.UserRepository.GetAllAsync(u=> friendIds.Contains(u.Id) || userIds.Contains(u.Id)));
            foreach (var friend in friendsMap)
            {
                if (pictures.Any(p => p.UserId == friend.Id && p.IsProfilePicture))
                    friend.ProfilPicture = pictures.Where(p => p.UserId == friend.Id && p.IsProfilePicture).FirstOrDefault().ImageName;
            }
            return friendsMap;
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
        public async Task ConfirmFriend(string id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var friend = await _unitOfWork.FriendRepository.GetAsync(u => u.UserId == userLoginId && u.FriendId == id);
            if (friend is null)
            {
                throw new NotFoundException("User is not found");
            }
            friend.Status = FriendRequestStatus.Accepted;
            _unitOfWork.FriendRepository.Update(friend);

            await _unitOfWork.SaveAsync();
        }
        public async Task AddFriendAsync(string id)
        {
            var userLoginId  = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == id);
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
                UserId = id,
                FriendId = userLoginId,
                Status = FriendRequestStatus.Pending
            };
            await _unitOfWork.FriendRepository.CreateAsync(userFriend);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeleteFriendAsync(string id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == id);
            if (user is null)
            {
                throw new NotFoundException("User is not found");
            }
            var userfriends = await _unitOfWork.FriendRepository.GetAsync(f => (f.UserId == userLoginId && f.FriendId == id) || 
                                                                               (f.UserId == id && f.FriendId == userLoginId));
            if (userfriends is null)
            {
                throw new NotFoundException("Friend is not found");
            }
            _unitOfWork.FriendRepository.Delete(userfriends);
            await _unitOfWork.SaveAsync();
        }
        public async Task FriendBlockAsync(string id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == id);
            if (user is null)
            {
                throw new NotFoundException("User is not found");
            }
            var userfriends = await _unitOfWork.FriendRepository.GetAsync(f => f.UserId == userLoginId && f.FriendId == id);
            if (userfriends is null)
            {
                throw new AlreadyException("Friend is not found");
            }
            userfriends.Status = FriendRequestStatus.Blocked;
            _unitOfWork.FriendRepository.Update(userfriends);
            await _unitOfWork.SaveAsync();
        }
        public async Task FriendUnBlockAsync(string id)
        {
            var UserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == id);
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
        public async Task Declined(string id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == id);
            if (user is null)
            {
                throw new NotFoundException("User is not found");
            }
            var userfriends = await _unitOfWork.FriendRepository.GetAsync(f => f.UserId == userLoginId && f.FriendId == id);
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
