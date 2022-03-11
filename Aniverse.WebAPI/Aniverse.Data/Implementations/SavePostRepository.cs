using Aniverse.Core.Entities;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;

namespace Aniverse.Data.Implementations
{
    public class SavePostRepository : Repository<SavePost>, ISavePostRepository
    {
        private readonly AppDbContext _context;
        public SavePostRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
