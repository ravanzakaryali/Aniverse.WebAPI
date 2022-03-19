﻿using Aniverse.Business.DTO_s.Story;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface IStoryService
    {
        Task<List<StoryGetDto>> GetAllAsync(HttpRequest request);
        Task<List<StoryGetDto>> GetUserAsync(string username, HttpRequest request);
        Task<List<StoryGetDto>> GetAllArchive(HttpRequest request, int page, int size);
        Task<List<StoryGetDto>> GetAllRecycle(HttpRequest request, int page, int size);
        Task CreateAsync(StoryCreateDto storyCreate);
        Task<List<StoryGetDto>> GetFriendAsync(HttpRequest request);
        Task DeleteAsync(int id);
        Task ArchiveAsync(int id);
    }
}
