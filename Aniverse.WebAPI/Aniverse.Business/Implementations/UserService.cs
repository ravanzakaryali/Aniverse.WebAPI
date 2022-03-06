using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.User;
using Aniverse.Business.Helpers;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Implementations
{
    public class UserService : IUserService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        private readonly IHostEnvironment _hostEnvironment;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        public async Task ChangeBio(JsonPatchDocument<AppUser> bioChange, ClaimsPrincipal user)
        {
            var userDb = await _unitOfWork.UserRepository.GetAsync(u => u.Id == user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value);
            bioChange.ApplyTo(userDb);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<UserAllDto>> GetAllAsync(ClaimsPrincipal user)
        {
            var userId = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            var friends = await _unitOfWork.FriendRepository.GetAllAsync(f=>f.UserId == userId);
            var friendsId = friends.Select(f=>f.FriendId);
            return _mapper.Map<List<UserAllDto>>(await _unitOfWork.UserRepository.GetAllAsync(u=> !friendsId.Contains(u.Id) && u.Id != userId));
        }
        public async Task<UserGetDto> GetAsync(string id, HttpRequest request)
        {
            var user = _mapper.Map<UserGetDto>(await _unitOfWork.UserRepository.GetAsync(u => u.UserName == id));
            var picture = await _unitOfWork.PictureRepository.GetAsync(p => p.User.UserName == id && p.IsProfilePicture == true);
            var cover = await _unitOfWork.PictureRepository.GetAsync(p => p.User.UserName == id && p.IsCoverPicture == true);
            if (picture != null)
            {
                user.ProfilPicture = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, picture.ImageName);
            }
            if (cover != null)
            {
                user.CoverPicture = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, cover.ImageName);

            }
            return user;
        }

        public async Task ProfileCreate(ProfileCreateDto profilCreate, ClaimsPrincipal user)
        {
            var userId = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            var picture = new Picture
            {
                IsProfilePicture = true,
                ImageName = await profilCreate.ImageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                UserId = userId,
            };
            var pictureProfileDb = await _unitOfWork.PictureRepository.GetAsync(p => p.UserId == userId && p.IsProfilePicture == true);
            await _unitOfWork.PictureRepository.CreateAsync(picture);
            if (pictureProfileDb != null)
            {
                pictureProfileDb.IsProfilePicture = false;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task CoverCreate(ProfileCreateDto coverCreate, ClaimsPrincipal user)
        {
            var userId = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            var picture = new Picture
            {
                IsCoverPicture = true,
                ImageName = await coverCreate.ImageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                UserId = userId,
            };
            var coverPictureDb = await _unitOfWork.PictureRepository.GetAsync(p => p.UserId == userId && p.IsCoverPicture == true);
            await _unitOfWork.PictureRepository.CreateAsync(picture);
            if (coverPictureDb != null)
            {
                coverPictureDb.IsCoverPicture = false;
            }
            await _unitOfWork.SaveAsync();
        }
        public async Task<List<GetPictureDto>> GetPhotos(string username, HttpRequest request, int page = 1, int size = 1)
        {
            var photos = await _unitOfWork.PictureRepository.GetAllPaginateAsync(page, size, p => p.User.UserName == username);
            var photosMap = _mapper.Map<List<GetPictureDto>>(photos);

            for (int i = 0; i < photosMap.Count; i++)
            {
                photosMap[i].ImageName = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, photos[i].ImageName);

            }
            return photosMap;
        }
    }
}
