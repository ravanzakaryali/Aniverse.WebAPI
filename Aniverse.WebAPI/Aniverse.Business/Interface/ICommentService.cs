using Aniverse.Business.DTO_s.Comment;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface ICommentService
    {
        Task CreateAsync(CommentCreateDto commentCreate);
        Task<List<CommentGetDto>> GetPostComments(int id);
        Task CommentDeleteAsync(int id);
    }
}
