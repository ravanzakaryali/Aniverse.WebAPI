using Aniverse.Business.DTO_s.StatusCode;
using Aniverse.Business.DTO_s.Story;
using Aniverse.Business.Interface;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class StoryController : Controller
    {
        private IUnitOfWorkService _unitOfWorkService { get; }
        public StoryController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet]
        public async Task<ActionResult<List<StoryGetDto>>> GetAllAsync()
        {
            return await _unitOfWorkService.StoryService.GetAllAsync();
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<List<StoryGetDto>>> GetUserStories(string username)
        {
            var request = HttpContext.Request;
            return await _unitOfWorkService.StoryService.GetUserAsync(username, request);
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] StoryCreateDto storyCreate)
        {
            try
            {
                await _unitOfWorkService.StoryService.CreateAsync(storyCreate);
                return StatusCode(StatusCodes.Status204NoContent,new Response { Status = "Successs", Message = "Story successfully posted"});
            }
            catch(Exception ex)
            {
               return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("friend")]
        public async Task<ActionResult<List<StoryGetDto>>> GetFriendStoriesAsync()
        {
            var request = HttpContext.Request;
            return await _unitOfWorkService.StoryService.GetFriendAsync(request);
        }

    }
}
