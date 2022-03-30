using Aniverse.Business.DTO_s.Comment;
using Aniverse.Business.Exceptions;
using Aniverse.Business.Extensions;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Implementations
{
    public class CommentService : ICommentService
    {

        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CommentGetDto>> GetPostComments(int id)
        {
            return _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c => c.PostId == id && c.CommentId == null, "ReplyComment", "User"));
        }
        public async Task<CommentGetDto> CreateAsync(CommentCreateDto commentCreate, HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            commentCreate.UserId = userLoginId;
            var comment = await _unitOfWork.CommentRepository.CreateComment(_mapper.Map<Comment>(commentCreate));
            await _unitOfWork.SaveAsync();
            var picture = await _unitOfWork.PictureRepository.GetAsync(p => p.UserId == userLoginId && p.IsProfilePicture);
            picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            var postMap = _mapper.Map<CommentGetDto>(comment);
            postMap.User.ProfilPicture = picture.ImageName;
            return postMap;
        }

        public async Task CommentDeleteAsync(int id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var commentDb = await _unitOfWork.CommentRepository.GetAsync(c => c.UserId == userLoginId && c.Id == id);
            if (commentDb is null)
            {
                throw new NotFoundException("Comment is not found");
            }
        }
        private void PictureDbName(List<Picture> pictures, HttpRequest request)
        {
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
        }
        private void CommentUserProfile(CommentGetDto postMap, List<Picture> pictures)
        {
            if (pictures.Any(p => p.UserId == postMap.UserId && p.IsProfilePicture))
                postMap.User.ProfilPicture = pictures.Where(p => p.UserId == postMap.UserId && p.IsProfilePicture).FirstOrDefault().ImageName;
        }
    }
}
