using Aniverse.Business.DTO_s.Comment;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface ICommentService
    {
        Task<CommentGetDto> CreateAsync(CommentCreateDto commentCreate, HttpRequest request);
        Task<List<CommentGetDto>> GetPostComments(int id);
        Task CommentDeleteAsync(int id);
    }
}
