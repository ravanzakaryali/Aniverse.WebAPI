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
    public class FriendRepository : Repository<UserFriend>, IFriendRepository
    {
        private AppDbContext _context { get;}
        public FriendRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<string>> GetFriendId(string id, FriendRequestStatus enumStatus = FriendRequestStatus.Accepted)
        {
            var friends = await _context.UserFriends.Where(u => u.UserId == id && u.Status == enumStatus).Select(u => u.FriendId).Distinct().ToListAsync();
            return friends;
        }
    }
}
