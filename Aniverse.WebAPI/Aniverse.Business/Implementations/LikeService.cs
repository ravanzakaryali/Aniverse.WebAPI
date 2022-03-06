using Aniverse.Business.DTO_s.Post.Like;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Implementations
{
    public class LikeService : ILikeService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;


        public LikeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateAsync(LikeCreateDto likeCreate, ClaimsPrincipal user)
        {
            var id = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            likeCreate.UserId = id;
            if (likeCreate.IsLike)
            {
                await _unitOfWork.LikeRepository.CreateAsync(_mapper.Map<Like>(likeCreate));
            }
            else
            {
               Like like = await _unitOfWork.LikeRepository.GetAsync(like => likeCreate.UserId == like.UserId && likeCreate.PostId == like.PostId);
                _unitOfWork.LikeRepository.Delete(like);
            }
            await _unitOfWork.SaveAsync();
        }
    }
}
