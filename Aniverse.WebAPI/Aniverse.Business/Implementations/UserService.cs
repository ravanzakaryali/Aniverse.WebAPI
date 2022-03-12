using Aniverse.Business.DTO_s.Picture;
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
            if(user is null)
            {
                throw new NotFoundException("User is not found");   
            }
            bioChange.ApplyTo(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<UserAllDto>> GetAllAsync()
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var friends = await _unitOfWork.FriendRepository.GetAllAsync(f=>f.UserId == userLoginId || f.FriendId== userLoginId);
            if(friends is null)
            {
                throw new NotFoundException("Friends is not found");
            }
            var friendsId = friends.Select(f=>f.FriendId);
            var usersId = friends.Select(f=>f.UserId);
            var users = await _unitOfWork.UserRepository.GetAllAsync(u => !friendsId.Contains(u.Id) && !usersId.Contains(u.Id) && u.Id != userLoginId);
            return _mapper.Map<List<UserAllDto>>(users);
        }
        public async Task<UserGetDto> GetAsync(string id, HttpRequest request)
        {
            var user = _mapper.Map<UserGetDto>(await _unitOfWork.UserRepository.GetAsync(u => u.UserName == id));
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
        public async Task<UserGetDto> GetLoginUser()
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            return _mapper.Map<UserGetDto>(await _unitOfWork.UserRepository.GetAsync(u => u.Id == userLoginId));
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
            var userLoginId  = _httpContextAccessor.HttpContext.User.GetUserId();
            var frineds = await _unitOfWork.FriendRepository.GetAllAsync(u => u.UserId == userLoginId && u.Status == FriendRequestStatus.Blocked, "Friend");
            if(frineds is null)
            {
                throw new NotFoundException("Friend is not found");
            }
            var friendsId = frineds.Select(f => f.FriendId);
            return _mapper.Map<List<UserGetDto>>(await _unitOfWork.UserRepository.GetAllAsync(u => friendsId.Contains(u.Id)));
        }
        public async Task<List<GetPictureDto>> GetPhotos(string username, HttpRequest request, int page = 1, int size = 1)
        {
            var photos = await _unitOfWork.PictureRepository.GetAllPaginateAsync(page, size, p => p.User.UserName == username);
            var photosMap = _mapper.Map<List<GetPictureDto>>(photos);

            for (int i = 0; i < photosMap.Count; i++)
            {
                photosMap[i].ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{photos[i].ImageName}");

            }
            return photosMap;
        }
        public async Task<List<GetPictureDto>> GetUserPhotos(string username, HttpRequest request, int page=1,int size = 1)
        {
            var photos = await _unitOfWork.PictureRepository.GetAllPaginateAsync(page, size, p => p.User.UserName == username && p.AnimalId == null);
            var photosMap = _mapper.Map<List<GetPictureDto>>(photos);

            for (int i = 0; i < photosMap.Count; i++)
            {
                photosMap[i].ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{photos[i].ImageName}");

            }
            return photosMap;
        }
        public async Task<List<UserAllDto>> SearchAsync(SearchDto search)
        {
            return _mapper.Map<List<UserAllDto>>(await _unitOfWork.UserRepository.GetAllAsync(u => u.UserName.Contains(search.SearchContent) || u.Firstname.Contains(search.SearchContent) || u.Lastname.Contains(search.SearchContent)));
        } 
    }
}
