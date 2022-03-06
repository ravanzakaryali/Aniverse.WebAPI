using Aniverse.Business.DTO_s.Friend;
using Aniverse.Business.DTO_s.User;
using Aniverse.Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.UI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]

    public class FriendController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public FriendController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet("[controller]")]
        public async Task<ActionResult<List<UserGetDto>>> GetAllAsync()
        {
            var user = HttpContext.User;
            return Ok(await _unitOfWorkService.FriendService.GetAllAsync(user));
        }
        [HttpGet("friendRequest")]
        public async Task<ActionResult<List<UserGetDto>>> GetFriendRequestAsync()
        {
            var user = HttpContext.User;
            return Ok(await _unitOfWorkService.FriendService.GetUserFriendRequestAsync(user));
        }
        [HttpPut("[controller]/request")]
        public async Task<ActionResult> ConfirmFriend(FriendConfirmDto friend)
        {
            try
            {
                var user = HttpContext.User;
                await _unitOfWorkService.FriendService.ConfirmFriend(user, friend);
                return StatusCode(StatusCodes.Status204NoContent, new { Status = "Successs", Message = "Confirm successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new { Status = "Error", Message = ex.ToString() });
            }
        }
    }
}
