using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.DTO_s.Post.Like;
using Aniverse.Business.DTO_s.StatusCode;
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

    public class PostController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public PostController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet]
        public async Task<ActionResult<List<PostGetDto>>> GetAllAsync()
        {
            var request = HttpContext.Request;
            return await _unitOfWorkService.PostService.GetAllAsync(request);  
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<PostGetDto>>> GetAsync(string id)
        {
            var request = HttpContext.Request;
            return await _unitOfWorkService.PostService.GetAsync(id, request);
        }
        [HttpGet("friend")]
        public async Task<ActionResult<List<PostGetDto>>> GetFrinedsPostAll([FromQuery] int page,[FromQuery] int size)
        {
            var request = HttpContext.Request;
            return await _unitOfWorkService.PostService.GetFriendPost(request, page,size);
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] PostCreateDto postCreate)
        {
            try
            {
            await _unitOfWorkService.PostService.CreateAsync(postCreate);
            return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Post successfully posted" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        } 
        [HttpPost("like")]
        public async Task<ActionResult> LikeCreateAsync([FromBody]LikeCreateDto likeCreate)
        {
            try
            {
                await _unitOfWorkService.LikeService.CreateAsync(likeCreate);
                return StatusCode(StatusCodes.Status204NoContent, new Response  { Status = "Successs", Message = "Like successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("save")]
        public async Task<ActionResult> SavePostAsync([FromBody] PostSaveDto postSaveDto)
        {
            try
            {
                await _unitOfWorkService.PostService.PostSaveAsync(postSaveDto);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult> PostUpdateAsync(int id, PostCreateDto postCreate)
        {
            try
            {
                await _unitOfWorkService.PostService.PostUpdateAsync(id, postCreate);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Post update success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPatch("delete/{id}")]
        public async Task<ActionResult> PostDeleteAsync(int id)
        {
            try
            {
                await _unitOfWorkService.PostService.PostDeleteAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Post delete success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}

