using Aniverse.Business.DTO_s.Product;
using Aniverse.Business.DTO_s.StatusCode;
using Aniverse.Business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public ProductController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet("category")]
        public async Task<ActionResult<List<ProductCategoryGetDto>>> GetAllCategoryAsync()
        {
            try
            {
                return Ok(await _unitOfWorkService.ProductService.GetProductCategories());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("create")]
        public async Task<ActionResult<ProductGetDto>> ProductCreateAsync([FromForm] ProductCreateDto productCreate)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.ProductService.ProductCreateAsync(productCreate, request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductGetDto>>> GetProductsAsync(int id, [FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.ProductService.GetProductsAsync(id, page, size, request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductGetDto>>> GetAllProduct([FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.ProductService.GetAllAsync(page, size, request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
