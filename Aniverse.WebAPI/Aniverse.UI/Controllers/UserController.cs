using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.User;
using Aniverse.Business.Interface;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aniverse.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : Controller
    {
        private IUnitOfWorkService _unitOfWorkService { get; }
        public UserController(IUnitOfWorkService userService)
        {
            _unitOfWorkService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserAllDto>>> GetAllAsync()
        {
            var user = HttpContext.User;
            return await _unitOfWorkService.UserService.GetAllAsync(user);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDto>> GetAsync(string id)
        {
            var request = HttpContext.Request;
            return Ok(await _unitOfWorkService.UserService.GetAsync(id, request));
        }
        [HttpPatch("bio")]
        public async Task<ActionResult> BioPatch([FromBody] JsonPatchDocument<AppUser> bioChange)
        {
            var user = HttpContext.User;
            await _unitOfWorkService.UserService.ChangeBio(bioChange, user);
            return NoContent();
        }
        [HttpPost("profile")]
        public async Task<ActionResult> ChangeProfileImage([FromForm] ProfileCreateDto profilePicture)
        {
            try
            {
                var user = HttpContext.User;
                await _unitOfWorkService.UserService.ProfileCreate(profilePicture, user);
                return StatusCode(StatusCodes.Status204NoContent, new { Status = "Successs", Message = "Story successfully posted" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new { Status = "Error", Message = ex.ToString() });
            }
        }
        [HttpPost("cover")]
        public async Task<ActionResult> ChangeCoverImage([FromForm] ProfileCreateDto coverPicture)
        {
            try
            {

                var user = HttpContext.User;
                await _unitOfWorkService.UserService.CoverCreate(coverPicture, user);
                return StatusCode(StatusCodes.Status204NoContent, new { Status = "Successs", Message = "Story successfully posted" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new { Status = "Error", Message = ex.ToString() });
            }
        }
        [HttpGet("photos/{id}")]
        public async Task<ActionResult<List<GetPictureDto>>> GetPhotos(string id,[FromQuery] int page, [FromQuery] int size)
        {
            var request = HttpContext.Request;
            return Ok(await _unitOfWorkService.UserService.GetPhotos(id,request,page,size));
        }
        [HttpGet("block")]
        public async Task<ActionResult<List<UserGetDto>>> GetBlockUsers()
        {
            var user = HttpContext.User;
            return Ok(await _unitOfWorkService.UserService.GetBlcokUsersAsync(user));
        }
    }
}
