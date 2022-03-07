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
        public async Task<ActionResult<List<UserGetDto>>> GetFriends(string username)
        {
            return Ok(await _unitOfWorkService.FriendService.GetAllAsync(username));
        }
        [HttpGet("friendrequest")]
        public async Task<ActionResult<List<UserGetDto>>> GetFriendRequestAsync()
        {
            return Ok(await _unitOfWorkService.FriendService.GetUserFriendRequestAsync());
        }
        [HttpPut("requestconfirm")]
        public async Task<ActionResult> ConfirmFriend(FriendConfirmDto friend)
        {
            try
            {
                await _unitOfWorkService.FriendService.ConfirmFriend(friend);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Confirm successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("add")]
        public async Task<ActionResult> AddFriend(FriendRequestDto addFriend)
        {
            try
            {
                await _unitOfWorkService.FriendService.AddFriendAsync(addFriend);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Add Friend successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("delete")]
        public async Task<ActionResult> DeleteFriend(FriendRequestDto deleteFriend)
        {
            try
            {
                await _unitOfWorkService.FriendService.DeleteFriendAsync(deleteFriend);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Delete friend successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("block")]
        public async Task<ActionResult> FriendBlock(FriendRequestDto friendBlock)
        {
            try
            {
                await _unitOfWorkService.FriendService.FriendBlockAsync(friendBlock);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Friend block successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        } 
        [HttpPost("unblock")]
        public async Task<ActionResult> FriendUnblock(FriendRequestDto frinedUnblock)
        {
            try
            {
                await _unitOfWorkService.FriendService.FriendUnBlockAsync(frinedUnblock);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Friend unblock successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
