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
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.StoryService.GetAllAsync(request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
            
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<List<StoryGetDto>>> GetUserStories(string username)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.StoryService.GetUserAsync(username, request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<List<StoryGetDto>>> CreateAsync([FromForm] StoryCreateDto storyCreate)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.StoryService.CreateAsync(storyCreate, request));
            }
            catch(Exception ex)
            {
               return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("friend")]
        public async Task<ActionResult<List<StoryGetDto>>> GetFriendStoriesAsync([FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.StoryService.GetFriendAsync(page,size,request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
            
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
        [HttpGet("archive")]
        public async Task<ActionResult<List<StoryGetDto>>> GetArchiveStory([FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.StoryService.GetAllArchive(request, page, size));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("recycle")]
        public async Task<ActionResult<List<StoryGetDto>>> GetRecycleStory([FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.StoryService.GetAllRecycle(request, page, size));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
