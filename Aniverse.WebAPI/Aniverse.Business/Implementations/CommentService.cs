using Aniverse.Business.DTO_s.Comment;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using AutoMapper;
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
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CommentGetDto>> GetAllAsync(int id)
        {
            return _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c=>c.PostId == id));
        }
        public async Task CreateAsync(CommentCreateDto commentCreate, ClaimsPrincipal user)
        {
            var id = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            commentCreate.UserId = id;
            await _unitOfWork.CommentRepository.CreateAsync(_mapper.Map<Comment>(commentCreate));
            await _unitOfWork.SaveAsync();
        }
    }
}
