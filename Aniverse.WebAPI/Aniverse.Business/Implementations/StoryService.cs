using Aniverse.Business.DTO_s.Story;
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
using System.Security.Claims;
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
            return _mapper.Map<List<StoryGetDto>>(await _unitOfWork.StoryRepository.GetAllAsync(null, "User"));
        }
        public async Task<List<StoryGetDto>> GetUserAsync(string username, HttpRequest request)
        {
            var stories = _mapper.Map<List<StoryGetDto>>(await _unitOfWork.StoryRepository.GetAllAsync(s=>s.User.UserName == username, "User"));
            foreach (var story in stories)
            {
                story.ImageSrc = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{story.StoryFileName}");
            }
            return stories;
        }
        public async Task<List<StoryGetDto>> GetFriendAsync(string username, HttpRequest request)
        {
            var stories = _mapper.Map<List<StoryGetDto>>(await _unitOfWork.StoryRepository.GetFriendStory(username));
            foreach (var story in stories)
            {
                story.ImageSrc = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{story.StoryFileName}");
            }    
            return stories;
        }
    }
}
