using Aniverse.Business.DTO_s.Comment;
using Aniverse.Business.Extensions;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
            return _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c=>c.PostId == id && c.CommentId == null, "ReplyComment","User"));
        }
        public async Task CreateAsync(CommentCreateDto commentCreate)
        {
            var UserLoginId  = _httpContextAccessor.HttpContext.User.GetUserId();
            commentCreate.UserId = UserLoginId;
            await _unitOfWork.CommentRepository.CreateAsync(_mapper.Map<Comment>(commentCreate));
            await _unitOfWork.SaveAsync();
        }
    }
}
