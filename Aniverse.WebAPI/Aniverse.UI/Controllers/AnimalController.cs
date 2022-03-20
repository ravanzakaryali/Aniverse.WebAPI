using Aniverse.Business.DTO_s.Animal;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post;
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
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.AnimalService.GetAsync(id, request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
            
        }
        [HttpGet]
        public async Task<ActionResult<List<AnimalAllDto>>> GetAllAsync([FromQuery] int page, [FromQuery] int size )
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.AnimalService.GetAllAsync(request, page, size));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("friends/{id}")]
        public async Task<ActionResult<List<AnimalAllDto>>> GetFriendAnimals(string id, [FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.AnimalService.GetFriendAnimals(request, id, page, size));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
            
        }
        [HttpGet("user/{id}")]
        public async Task<ActionResult<List<AnimalAllDto>>> GetAnimalUserAsync(string id)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.AnimalService.GetAnimalUserAsync(id, request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("post/{id}")]
        public async Task<ActionResult<List<PostGetDto>>> GetAnimalPosts(string id)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.PostService.GetAnimalPosts(id, request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("follow/{id}")]
        public async Task<ActionResult> FollowCreate(int id,[FromBody] FollowDto follow)
        {
            try
            {
                await _unitOfWorkService.AnimalService.FollowCreate(id,follow);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "User follow successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("category")]
        public async Task<ActionResult<List<AnimalGetCategory>>> GetAnimalCategory()
        {
            try
            {
                return Ok(await _unitOfWorkService.AnimalService.GetAnimalCategory());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
            
        }
        [HttpPost("create")]
        public async Task<ActionResult> AnimalCreateAsync([FromBody] AnimalCreateDto animalCreate)
        {
            try
            {
                await _unitOfWorkService.AnimalService.AnimalCreateAsync(animalCreate);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Animal create successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message =  ex.Message });
            }
        }
        [HttpGet("select")]
        public async Task<ActionResult<List<AnimalSelectGetDto>>> GetSelectAnimal()
        {
            try
            {
                return Ok(await _unitOfWorkService.AnimalService.SelectAnimal());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
            
        } 
        [HttpPut("update/{id}")]    
        public async Task<ActionResult> UpdateAnimal(int id, AnimalUpdateDto animalUpdate)
        {
            try
            {
                await _unitOfWorkService.AnimalService.UpdateAnimalAsync(id, animalUpdate);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Animal update successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("photos/{animalname}")]
        public async Task<ActionResult<List<GetPictureDto>>> GetAnimalPhotos(string animalname, [FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.AnimalService.GetAnimalPhotos(animalname, request, page, size));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
            
        }
        [HttpPost("coverPicture/{id}")]
        public async Task<ActionResult> ChangeCoverPicture(int id, [FromForm] AnimalPictureChangeDto cover)
        {
            try
            {
                await _unitOfWorkService.AnimalService.ChangeCoverPicture(id, cover);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Animal cover pictre change successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("profilePicture/{id}")]
        public async Task<ActionResult> ChangeProfilePicture(int id, [FromForm] AnimalPictureChangeDto profile)
        {
            try
            {
                await _unitOfWorkService.AnimalService.ChangeProfilePicture(id, profile);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Animal cover pictre change successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        
    }
}
