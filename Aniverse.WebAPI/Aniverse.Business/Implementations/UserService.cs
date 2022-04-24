using Aniverse.Business.DTO_s.Page;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.DTO_s.User;
using Aniverse.Business.Exceptions;
using Aniverse.Business.Extensions;
using Aniverse.Business.Helpers;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using Aniverse.Core.Entites.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aniverse.Business.Implementations
{
    public class UserService : IUserService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        private readonly IHostEnvironment _hostEnvironment;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ChangeBio(JsonPatchDocument<AppUser> bioChange)
        {
            string userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            AppUser user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == userLoginId);
            if (user is null)
            {
                throw new NotFoundException("User is not found");
            }
            bioChange.ApplyTo(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<UserAllDto>> GetAllAsync(HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var friends = await _unitOfWork.FriendRepository.GetAllAsync(f => f.UserId == userLoginId || f.FriendId == userLoginId);
            if (friends is null)
            {
                throw new NotFoundException("Friends is not found");
            }
            var friendIds = friends.Select(f => f.FriendId);
            var userIds = friends.Select(f => f.UserId);
            var users = _mapper.Map<List<UserAllDto>>(await _unitOfWork.UserRepository.GetAllAsync(u => !friendIds.Contains(u.Id) && !userIds.Contains(u.Id) && u.Id != userLoginId));
            var returnUserIds = users.Select(u => u.Id);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => returnUserIds.Contains(p.UserId));
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            foreach (var user in users)
            {
                if (pictures.Any(p => p.UserId == user.Id && p.IsProfilePicture))
                    user.ProfilPicture = pictures.Where(p => p.UserId == user.Id && p.IsProfilePicture).FirstOrDefault().ImageName;
            }
            return users;
        }
        public async Task<UserGetDto> GetAsync(string id, HttpRequest request)
        {
            string userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = _mapper.Map<UserGetDto>(await _unitOfWork.UserRepository.GetAsync(u => u.UserName == id));
            user.FriendCount = await _unitOfWork.FriendRepository.GetTotalCountAsync(u => (u.UserId == user.Id || u.FriendId == user.Id) && u.Status == FriendRequestStatus.Accepted);
            var picture = await _unitOfWork.PictureRepository.GetAsync(p => p.User.UserName == id && p.IsProfilePicture == true);
            var cover = await _unitOfWork.PictureRepository.GetAsync(p => p.User.UserName == id && p.IsCoverPicture == true);
            if (picture != null)
            {
                user.ProfilPicture = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            if (cover != null)
            {
                user.CoverPicture = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{cover.ImageName}");

            }
            return user;
        }
        public async Task<UserGetDto> GetLoginUser(HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => p.UserId == userLoginId && (p.IsProfilePicture || p.IsCoverPicture));
            var userLogin = _mapper.Map<UserGetDto>(await _unitOfWork.UserRepository.GetAsync(u => u.Id == userLoginId));
            foreach (var picture in pictures)
            {
                if (picture.IsProfilePicture)
                {
                    userLogin.ProfilPicture = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
                }
                if (picture.IsCoverPicture)
                {
                    userLogin.CoverPicture = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
                }
            }
            return userLogin;
        }
        public async Task ProfileCreate(ProfileCreateDto profilCreate)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var picture = new Picture
            {
                IsProfilePicture = true,
                ImageName = await profilCreate.ImageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                UserId = userLoginId,
            };
            var pictureProfileDb = await _unitOfWork.PictureRepository.GetAsync(p => p.UserId == userLoginId && p.IsProfilePicture == true);
            await _unitOfWork.PictureRepository.CreateAsync(picture);
            if (pictureProfileDb != null)
            {
                pictureProfileDb.IsProfilePicture = false;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task CoverCreate(ProfileCreateDto coverCreate)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var picture = new Picture
            {
                IsCoverPicture = true,
                ImageName = await coverCreate.ImageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                UserId = userLoginId,
            };
            var coverPictureDb = await _unitOfWork.PictureRepository.GetAsync(p => p.UserId == userLoginId && p.IsCoverPicture == true);
            await _unitOfWork.PictureRepository.CreateAsync(picture);
            if (coverPictureDb != null)
            {
                coverPictureDb.IsCoverPicture = false;
            }
            await _unitOfWork.SaveAsync();
        }
        public async Task<List<UserGetDto>> GetBlcokUsersAsync()
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var frineds = await _unitOfWork.FriendRepository.GetAllAsync(u => u.UserId == userLoginId && u.Status == FriendRequestStatus.Blocked, "Friend");
            if (frineds is null)
            {
                throw new NotFoundException("Friend is not found");
            }
            var friendsId = frineds.Select(f => f.FriendId);
            return _mapper.Map<List<UserGetDto>>(await _unitOfWork.UserRepository.GetAllAsync(u => friendsId.Contains(u.Id)));
        }
        public async Task<List<GetPictureDto>> GetPhotos(string username, HttpRequest request, int page = 1, int size = 1)
        {
            var photos = await _unitOfWork.PictureRepository.GetAllPaginateAsync(page, size, p => p.Id, p => p.User.UserName == username);
            var photosMap = _mapper.Map<List<GetPictureDto>>(photos);

            for (int i = 0; i < photosMap.Count; i++)
            {
                photosMap[i].ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{photos[i].ImageName}");

            }
            return photosMap;
        }
        public async Task<List<GetPictureDto>> GetUserPhotos(string username, HttpRequest request, int page = 1, int size = 1)
        {
            var photos = await _unitOfWork.PictureRepository.GetAllPaginateAsync(page, size, p => p.Id, p => p.User.UserName == username && p.AnimalId == null && p.PageId == null);
            var photosMap = _mapper.Map<List<GetPictureDto>>(photos);

            for (int i = 0; i < photosMap.Count; i++)
            {
                photosMap[i].ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{photos[i].ImageName}");

            }
            return photosMap;
        }
        public async Task<List<UserAllDto>> SearchAsync(string search)
        {
            return _mapper.Map<List<UserAllDto>>(await _unitOfWork.UserRepository.GetAllAsync(u => u.UserName.Contains(search) || u.Firstname.Contains(search) || u.Lastname.Contains(search)));
        }
        public async Task<List<PageGetDto>> GetUserPages(string id, HttpRequest request)
        {
            var pages = _mapper.Map<List<PageGetDto>>(await _unitOfWork.PageRepository.GetAllAsync(p => p.UserId == id, "PageFollow"));
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => p.IsPageProfilePicture, "Page");
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            foreach (var pageGet in pages)
            {
                if (pictures.Any(p => pageGet.Id == p.PageId && p.IsPageProfilePicture))
                    pageGet.ProfilPicture = pictures.Where(p => pageGet.Id == p.Page.Id && p.IsPageProfilePicture).FirstOrDefault().ImageName;
            }
            return pages;
        }
    }
}
