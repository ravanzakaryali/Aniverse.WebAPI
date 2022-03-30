using Aniverse.Core.Entities;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;

namespace Aniverse.Data.Implementations
{
    public class SaveProductRepository : Repository<SaveProduct>, ISaveProductRepository
    {
        private AppDbContext _context;
        public SaveProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
