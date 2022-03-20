using Aniverse.Business.DTO_s.Page;
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
                return Ok(await _unitOfWorkService.PageService.GetAllAsync(page,size, request));
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
        public async Task<ActionResult<List<GetPictureDto>>> GetPhotos(string pagename)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.PageService.GetPhotos(pagename, request));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });

            }
        }
    }
}
