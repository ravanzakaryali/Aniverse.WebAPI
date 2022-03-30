using Aniverse.Business.DTO_s.Page;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.DTO_s.StatusCode;
using Aniverse.Business.DTO_s.User;
using Aniverse.Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PageController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public PageController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet]
        public async Task<ActionResult<List<PageGetDto>>> GetAllPages([FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.PageService.GetAllAsync(page, size, request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("{pagename}")]
        public async Task<ActionResult<PageGetDto>> GetPage(string pagename)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.PageService.GetPageAsync(pagename, request));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });

            }
        }
        [HttpGet("photos/{pagename}")]
        public async Task<ActionResult<List<GetPictureDto>>> GetPhotos(string pagename, [FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.PageService.GetPhotos(pagename, page, size, request));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });

            }
        }
        [HttpPost("create")]
        public async Task<ActionResult> PostCreateAsync(PageCreateDto pageCreate)
        {
            try
            {
                await _unitOfWorkService.PageService.PageCreateAsync(pageCreate);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Post create successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("posts/{pagename}")]
        public async Task<ActionResult<List<PostPageGetDto>>> GetPosts([FromQuery] int page, [FromQuery] int size, string pagename)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.PageService.GetPostsAsync(page, size, pagename, request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("profilPicture/{id}")]
        public async Task<ActionResult> ChangeProfileImage([FromForm] ProfileCreateDto profileCreate, int id)
        {
            try
            {

                await _unitOfWorkService.PageService.ProfileCreate(id, profileCreate);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Change picture successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("coverPicture/{id}")]
        public async Task<ActionResult> ChangeCoverImage(int id, [FromForm] ProfileCreateDto profileCreate)
        {
            try
            {

                await _unitOfWorkService.PageService.CoverCreate(id, profileCreate);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Change cover successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("{id}/followers")]
        public async Task<ActionResult<List<UserGetDto>>> GetFollowers(int id, [FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.PageService.GetPageFollowersUser(id, page, size, request));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });

            }
        }
        [HttpPost("follow/{id}")]
        public async Task<ActionResult> PageFollow(int id)
        {
            try
            {
                await _unitOfWorkService.PageService.PageFollow(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Follow successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("unfollow/{id}")]
        public async Task<ActionResult> PageUnfollow(int id)
        {
            try
            {
                await _unitOfWorkService.PageService.PageUnfollow(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Unfollow successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("update/{id}")]
        public async Task<ActionResult> PageUpdate(int id, PageUpdateDto pageUpdate)
        {
            try
            {
                await _unitOfWorkService.PageService.PageUpdate(id,pageUpdate);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Page update successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
