using Aniverse.Business.DTO_s.Animal;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.StatusCode;
using Aniverse.Business.DTO_s.User;
using Aniverse.Business.Interface;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public UserController(IUnitOfWorkService userService)
        {
            _unitOfWorkService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserAllDto>>> GetAllAsync()
        {
            return await _unitOfWorkService.UserService.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDto>> GetAsync(string id)
        {
            var request = HttpContext.Request;
            return Ok(await _unitOfWorkService.UserService.GetAsync(id, request));
        }
        [HttpGet("login")]
        public async Task<ActionResult<UserGetDto>> GetLoginUser()
        {
            return Ok(await _unitOfWorkService.UserService.GetLoginUser());
        }
        [HttpPatch("bio")]
        public async Task<ActionResult> BioUpdate([FromBody] JsonPatchDocument<AppUser> bioChange)
        {
            await _unitOfWorkService.UserService.ChangeBio(bioChange);
            return NoContent();
        }
        [HttpPost("profile")]
        public async Task<ActionResult> ChangeProfileImage([FromForm] ProfileCreateDto profilePicture)
        {
            try
            {
                await _unitOfWorkService.UserService.ProfileCreate(profilePicture);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Story successfully posted" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response{ Status = "Error", Message = ex.ToString() });
            }
        }
        [HttpPost("cover")]
        
        public async Task<ActionResult> ChangeCoverImage([FromForm] ProfileCreateDto coverPicture)
        {
            try
            {

                await _unitOfWorkService.UserService.CoverCreate(coverPicture);
                return StatusCode(StatusCodes.Status204NoContent, new { Status = "Successs", Message = "Story successfully posted" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new { Status = "Error", Message = ex.ToString() });
            }
        }
        [HttpGet("block")]
        public async Task<ActionResult<List<UserGetDto>>> GetBlockUsers()
        {
            return Ok(await _unitOfWorkService.UserService.GetBlcokUsersAsync());
        }
        [HttpGet("{username}/follows/animal")]
        public async Task<ActionResult<List<AnimalAllDto>>> AnimalUserFollows(string username)
        {
            try
            {
                return Ok(await _unitOfWorkService.AnimalService.AnimalUserFollows(username));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("photos/{id}")]
        public async Task<ActionResult<List<GetPictureDto>>> GetPhotos(string id, [FromQuery] int page, [FromQuery] int size)
        {
            var request = HttpContext.Request;
            return Ok(await _unitOfWorkService.UserService.GetPhotos(id, request, page, size));
        }
        [HttpGet("only/photos/{id}")]
        public async Task<ActionResult<List<GetPictureDto>>> GetUserPhotos(string id, [FromQuery] int page, [FromQuery] int size)
        {
            var request = HttpContext.Request;
            return Ok(await _unitOfWorkService.UserService.GetUserPhotos(id, request, page, size));
        }
        [HttpGet("search")]
        public async Task<ActionResult<List<UserAllDto>>> SearchAsync([FromQuery] string search)
        {
            try
            {
                return Ok(await _unitOfWorkService.UserService.SearchAsync(search));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
