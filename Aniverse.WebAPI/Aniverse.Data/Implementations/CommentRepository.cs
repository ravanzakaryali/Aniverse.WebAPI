using Aniverse.Core.Entites;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;

namespace Aniverse.Data.Implementations
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
