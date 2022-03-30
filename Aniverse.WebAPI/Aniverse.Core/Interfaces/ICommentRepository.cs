using Aniverse.Core.Entites;
using System.Threading.Tasks;

namespace Aniverse.Core.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment> CreateComment(Comment comment);
    }
}
