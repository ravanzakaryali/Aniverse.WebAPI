using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.DTO_s.Post.Like;
using Aniverse.Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.UI.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]

    public class PostController : Controller
    {
        private IUnitOfWorkService _unitOfWorkService { get; }
        public PostController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet("[controller]")]
        public async Task<ActionResult<List<PostGetDto>>> GetAllAsync()
        {
            var request = HttpContext.Request;
            return await _unitOfWorkService.PostService.GetAllAsync(request);  
        }
        [HttpGet("[controller]/{id}")]
        public async Task<ActionResult<List<PostGetDto>>> GetAsync(string id)
        {
            var request = HttpContext.Request;
            return await _unitOfWorkService.PostService.GetAsync(id, request);
        }
        [HttpGet("user/friend/[controller]")]
        public async Task<ActionResult<List<PostGetDto>>> GetAllAsync([FromQuery] int page,[FromQuery] int size)
        {
            var request = HttpContext.Request;
            var user = HttpContext.User;
            return await _unitOfWorkService.PostService.GetFriendPost(user,request, page,size);
        }
        [HttpPost("[controller]")]
        public async Task<ActionResult> Create([FromForm] PostCreateDto postCreate)
        {
            try
            {
                var user = HttpContext.User;
            await _unitOfWorkService.PostService.CreateAsync(postCreate, user);
            return StatusCode(StatusCodes.Status204NoContent, new { Status = "Successs", Message = "Post successfully posted" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new { Status = "Error", Message = ex.ToString() });
            }
        } 
        [HttpPost("[controller]/like")]
        public async Task<ActionResult> LikeCreate([FromBody]LikeCreateDto likeCreate)
        {
            try
            {
                var user = HttpContext.User;
                await _unitOfWorkService.LikeService.CreateAsync(likeCreate, user);
                return StatusCode(StatusCodes.Status204NoContent, new { Status = "Successs", Message = "Like successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new { Status = "Error", Message = ex.ToString() });
            }
        }
    }
}

