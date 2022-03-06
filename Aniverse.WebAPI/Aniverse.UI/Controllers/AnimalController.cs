using Aniverse.Business.DTO_s.Animal;
using Aniverse.Business.DTO_s.Post;
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
    public class AnimalController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public AnimalController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalGetDto>> GetAsync(string id)
        {
            return await _unitOfWorkService.AnimalService.GetAsync(id);
        }
        public async Task<ActionResult<List<AnimalAllDto>>> GetAllAsync()
        {
            return await _unitOfWorkService.AnimalService.GetAllAsync();
        }
        [HttpGet("friends/{id}")]
        public async Task<ActionResult<List<AnimalAllDto>>> GetFriendAsync(string id)
        {
            return Ok(await _unitOfWorkService.AnimalService.GetFriendAsync(id));
        }
        [HttpGet("user/{username}")]
        public async Task<ActionResult<List<AnimalAllDto>>> GetAnimalUserAsync(string username)
        {
            return Ok(await _unitOfWorkService.AnimalService.GetAnimalUserAsync(username));
        }
        [HttpGet("post/{id}")]
        public async Task<ActionResult<List<PostGetDto>>> GetAnimalPosts(string id)
        {
            var request = HttpContext.Request;
            return Ok(await _unitOfWorkService.AnimalService.GetAnimalPosts(id, request));
        }
        [HttpPost("follow")]
        public async Task<ActionResult> FollowCreate([FromBody] FollowDto follow)
        {
            try
            {
                var user = HttpContext.User;
                await _unitOfWorkService.AnimalService.FollowCreate(follow, user);
                return StatusCode(StatusCodes.Status204NoContent, new { Status = "Successs", Message = "User follow successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new { Status = "Error", Message = ex.ToString() });
            }
        }
        [HttpGet("category")]
        public async Task<ActionResult<List<AnimalGetCategory>>> GetAnimalCategory()
        {
            return Ok(await _unitOfWorkService.AnimalService.GetAnimalCategory());
        }
        [HttpPost("create")]
        public async Task<ActionResult> AnimalCreateAsync([FromBody] AnimalCreateDto animalCreate)
        {
            try
            {
                var user = HttpContext.User;
                await _unitOfWorkService.AnimalService.AnimalCreateAsync(animalCreate,user);
                return StatusCode(StatusCodes.Status204NoContent, new { Status = "Successs", Message = "Animal create successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new { Status = "Error", ex.Message });
            }
        }
        [HttpGet("select")]
        public async Task<ActionResult<List<AnimalSelectGetDto>>> SelectAnimalDto()
        {
            var user = HttpContext.User;
            return await _unitOfWorkService.AnimalService.SelectAnimal(user);
        } 
        [HttpPut("update/{id}")]    
        public async Task<ActionResult> UpdateAnimal(int id, AnimalUpdateDto animalUpdate)
        {
            try
            {
                var user = HttpContext.User;
                await _unitOfWorkService.AnimalService.UpdateAnimalAsync(id, animalUpdate, user);
                return StatusCode(StatusCodes.Status204NoContent, new { Status = "Successs", Message = "Animal update successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new { Status = "Error", ex.Message });
            }
        }
    }
}
