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
    [Route("api")]
    [ApiController]
    [Authorize]

    public class StoryController : Controller
    {
        private IUnitOfWorkService _unitOfWorkService { get; }
        public StoryController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet("[controller]")]
        public async Task<ActionResult<List<StoryGetDto>>> GetAllAsync()
        {
            return await _unitOfWorkService.StoryService.GetAllAsync();
        }
        [HttpGet("[controller]/{username}")]
        public async Task<ActionResult<List<StoryGetDto>>> GetAllAsync(string username)
        {
            var request = HttpContext.Request;
            return await _unitOfWorkService.StoryService.GetUserAsync(username, request);
        }
        [HttpPost("[controller]")]
        public async Task<ActionResult> CreateAsync([FromForm] StoryCreateDto storyCreate)
        {
            try
            {
                var user = HttpContext.User;
                await _unitOfWorkService.StoryService.CreateAsync(storyCreate, user);
                return StatusCode(StatusCodes.Status204NoContent,new { Status = "Successs", Message = "Story successfully posted"});
            }
            catch(Exception ex)
            {
               return StatusCode(StatusCodes.Status502BadGateway, new { Status = "Error", Message = ex.ToString() });
            }
        }
        [HttpGet("user/{username}/friend/[controller]")]
        public async Task<ActionResult<List<StoryGetDto>>> GetFriendAsync(string username)
        {
            var request = HttpContext.Request;
            return await _unitOfWorkService.StoryService.GetFriendAsync(username, request);
        }

    }
}
