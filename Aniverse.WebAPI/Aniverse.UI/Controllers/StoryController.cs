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
        private readonly IUnitOfWorkService _unitOfWorkService;
        public StoryController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet]
        public async Task<ActionResult<List<StoryGetDto>>> GetAllAsync()
        {
            var request = HttpContext.Request;
            return await _unitOfWorkService.StoryService.GetAllAsync(request);
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

        [HttpPatch("delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWorkService.StoryService.DeleteAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Story delete successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPatch("archive/{id}")]
        public async Task<ActionResult> Archive(int id)
        {
            try
            {
                await _unitOfWorkService.StoryService.ArchiveAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Story archive successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
