using Aniverse.Core.Entites;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;

namespace Aniverse.Data.Implementations
{
    public class AnimalCategoryRepository : Repository<AnimalCategory>, IAnimalCategory
    {
        private readonly AppDbContext _context;
        public AnimalCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
