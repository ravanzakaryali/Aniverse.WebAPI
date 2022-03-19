﻿using Aniverse.Business.DTO_s.Story;
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
        public async Task<List<StoryGetDto>> GetAllAsync(HttpRequest request)
        {
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(u => u.IsProfilePicture);
            PictureNameDb(pictures, request);
            var stories = _mapper.Map<List<StoryGetDto>>(await _unitOfWork.StoryRepository.GetAllAsync(s => !s.IsDeleted && !s.IsArchive, "User"));
            UserStoryPictureNameDb(pictures, request, stories);
            return stories;
        }
        public async Task<List<StoryGetDto>> GetUserAsync(string username, HttpRequest request)
        {
            var stories = _mapper.Map<List<StoryGetDto>>(await _unitOfWork.StoryRepository.GetAllAsync(s => s.User.UserName == username && !s.IsDeleted && !s.IsArchive, "User"));
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
            PictureNameDb(pictures, request);
            var stories = _mapper.Map<List<StoryGetDto>>(await _unitOfWork.StoryRepository.GetAllAsync(s => !s.IsDeleted && !s.IsArchive && friendsId.Contains(s.UserId) || !s.IsDeleted && !s.IsArchive && usersId.Contains(s.UserId), "User"));
            UserStoryPictureNameDb(pictures, request, stories);
            return stories;
        }
        public async Task<List<StoryGetDto>> GetAllArchive(HttpRequest request, int page, int size)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var stories = _mapper.Map<List<StoryGetDto>>(await _unitOfWork.StoryRepository.GetAllPaginateAsync(page, size, p => p.CreatedDate, p => p.UserId == userLoginId && p.IsArchive == true));
            if (stories is null)
            {
                throw new NotFoundException("Story is not found");
            }
            UserStoryPictureNameDb(null,request, stories);
            return stories;

        }
        public async Task<List<StoryGetDto>> GetAllRecycle(HttpRequest request, int page, int size)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var stories = _mapper.Map<List<StoryGetDto>>(await _unitOfWork.StoryRepository.GetAllPaginateAsync(page, size, p => p.CreatedDate, p => p.UserId == userLoginId && p.IsDeleted == true));
            if (stories is null)
            {
                throw new NotFoundException("Story is not found");
            }
            UserStoryPictureNameDb(null, request, stories);
            return stories;

        }
        public async Task DeleteAsync(int id)
        {
            string userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var story = await _unitOfWork.StoryRepository.GetAsync(s => s.Id == id && s.UserId == userLoginId);
            if (story is null)
            {
                throw new NotFoundException("Story is not found");
            }
            story.IsDeleted = true;
            await _unitOfWork.SaveAsync();
        }
        public async Task ArchiveAsync(int id)
        {
            string userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var story = await _unitOfWork.StoryRepository.GetAsync(s => s.Id == id && s.UserId == userLoginId);
            if (story is null)
            {
                throw new NotFoundException("Story is not found");
            }
            story.IsArchive = true;
            await _unitOfWork.SaveAsync();
        }

        private void PictureNameDb(List<Picture> pictures, HttpRequest request)
        {
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
        }
        private void UserStoryPictureNameDb(List<Picture> pictures, HttpRequest request, List<StoryGetDto> stories)
        {
            foreach (var story in stories)
            {
                if (pictures != null)
                    if (pictures.Any(p => p.UserId == story.User.Id))
                    {
                        story.User.ProfilPicture = pictures.Where(p => p.UserId == story.User.Id).First().ImageName;
                    }

                story.ImageSrc = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{story.StoryFileName}");
            }
        }
    }
}
