using Aniverse.Core.Entites;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;

namespace Aniverse.Data.Implementations
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private AppDbContext _context;
        public MessageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
