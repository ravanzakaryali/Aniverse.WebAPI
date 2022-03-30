using Aniverse.Core.Entites;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Post> CreatePost(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return await _context.Posts
                .Where(p => p.Id == post.Id)
                .Include(p =>p.User)
                .Include(p=>p.Animal)
                .FirstOrDefaultAsync();

        }
    }
}
