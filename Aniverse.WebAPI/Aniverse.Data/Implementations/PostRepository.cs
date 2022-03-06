using Aniverse.Core.Entites;
using Aniverse.Core.Entites.Enum;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aniverse.Data.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private AppDbContext _context;
        public PostRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
