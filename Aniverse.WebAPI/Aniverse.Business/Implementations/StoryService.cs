using Aniverse.Business.DTO_s.Story;
using Aniverse.Business.Exceptions;
using Aniverse.Business.Extensions;
using Aniverse.Business.Helpers;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aniverse.Business.Implementations
{
    public class StoryService : IStoryService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly IHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StoryService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(StoryCreateDto storyCreate)
        {
            storyCreate.UserId = _httpContextAccessor.HttpContext.User.GetUserId();
            storyCreate.StoryFileName = await storyCreate.StoryFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images");
            await _unitOfWork.StoryRepository.CreateAsync(_mapper.Map<Story>(storyCreate));
            await _unitOfWork.SaveAsync();
        }
        public async Task<List<StoryGetDto>> GetAllAsync()
        {
            return _mapper.Map<List<StoryGetDto>>(await _unitOfWork.StoryRepository.GetAllAsync(s=>!s.IsDeleted, "User"));
        }
        public async Task<List<StoryGetDto>> GetUserAsync(string username, HttpRequest request)
        {
            var stories = _mapper.Map<List<StoryGetDto>>(await _unitOfWork.StoryRepository.GetAllAsync(s => s.User.UserName == username && !s.IsDeleted && !s.IsArchive ,"User"));
            foreach (var story in stories)
            {
                story.ImageSrc = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{story.StoryFileName}");
            }
            return stories;
        }
        public async Task<List<StoryGetDto>> GetFriendAsync(HttpRequest request)
        {
            string userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var friends = await _unitOfWork.FriendRepository.GetAllAsync(f => f.UserId == userLoginId || f.FriendId == userLoginId);
            if (friends is null)
            {
                throw new NotFoundException("Friends is not found");
            }
            var usersId = friends.Select(f => f.UserId);
            var friendsId = friends.Select(f => f.FriendId);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(u => u.IsProfilePicture && usersId.Contains(u.UserId) && friendsId.Contains(u.UserId));
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            var stories = _mapper.Map<List<StoryGetDto>>(await _unitOfWork.StoryRepository.GetAllAsync(s => !s.IsDeleted && !s.IsArchive && friendsId.Contains(s.UserId) || usersId.Contains(s.UserId), "User"));
            foreach (var story in stories)
            {
                if (pictures.Any(p => p.UserId == story.User.Id))
                {
                    story.User.ProfilPicture = pictures.Where(p => p.UserId == story.User.Id).First().ImageName;
                }

                story.ImageSrc = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{story.StoryFileName}");
            }
            return stories;
        }
    }
}
