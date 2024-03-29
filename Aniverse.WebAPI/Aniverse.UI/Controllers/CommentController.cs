﻿using Aniverse.Business.DTO_s.Comment;
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
    public class CommentController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public CommentController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpPost]
        public async Task<ActionResult<CommentGetDto>> Create([FromBody] CommentCreateDto commentCreate)
        {
            try
            {
                var request = HttpContext.Request;
                return Ok(await _unitOfWorkService.CommentService.CreateAsync(commentCreate, request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new  Response { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentGetDto>> GetCommentAsync(int id)
        {
            try
            {
                return Ok(await _unitOfWorkService.CommentService.GetPostComments(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
            
        }
        [HttpPatch("delete/{id}")]
        public async Task<ActionResult> CommentDeleteAsync(int id)
        {
            try
            {
                await _unitOfWorkService.CommentService.CommentDeleteAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new Response { Status = "Successs", Message = "Comment delete successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
