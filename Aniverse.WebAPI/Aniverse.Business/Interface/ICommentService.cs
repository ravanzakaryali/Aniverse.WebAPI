using Aniverse.Business.DTO_s.Comment;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface ICommentService
    {
        Task<List<CommentGetDto>> GetAllAsync(int id);
        Task CreateAsync(CommentCreateDto commentCreate);
    }
}
