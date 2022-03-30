using Aniverse.Business.DTO_s.Post.Like;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface ILikeService
    {
        Task<string> CreateAsync(LikeCreateDto likeCreate);
    }
}
