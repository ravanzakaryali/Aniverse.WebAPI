    using Aniverse.Business.DTO_s.Friend;
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

    public class FriendController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public FriendController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<List<UserGetDto>>> GetAllFriends(string username, [FromQuery] int page, [FromQuery] int size)
        {
            var request = HttpContext.Request;
            return Ok(await _unitOfWorkService.FriendService.GetAllAsync(username,request,page,size));
        }
        [HttpGet("request")]
        public async Task<ActionResult<List<UserGetDto>>> GetFriendRequestAsync()
        {
            return Ok(await _unitOfWorkService.FriendService.GetUserFriendRequestAsync());
        }
        [HttpPost("confirm/{id}")]
        public async Task<ActionResult> ConfirmFriend(string id)
        {
            try
            {
                await _unitOfWorkService.FriendService.ConfirmFriend(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Confirm successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("add/{id}")]
        public async Task<ActionResult> AddFriend(string id)
        {
            try
            {
                await _unitOfWorkService.FriendService.AddFriendAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Add Friend successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("delete/{id}")]
        public async Task<ActionResult> DeleteFriend(string id)
        {
            try
            {
                await _unitOfWorkService.FriendService.DeleteFriendAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Delete friend successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("block/{id}")]
        public async Task<ActionResult> FriendBlock(string id)
        {
            try
            {
                await _unitOfWorkService.FriendService.FriendBlockAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Friend block successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        } 
        [HttpPost("unblock/{id}")]
        public async Task<ActionResult> FriendUnblock(string id)
        {
            try
            {
                await _unitOfWorkService.FriendService.FriendUnBlockAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Friend unblock successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("declined/{id}")]
        public async Task<ActionResult> Declined(string id)
        {
            try
            {
                await _unitOfWorkService.FriendService.Declined(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Friend declined successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
