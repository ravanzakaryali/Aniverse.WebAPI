using Aniverse.Core.Entites;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;

namespace Aniverse.Data.Implementations
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly AppDbContext _context;
        public LikeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
