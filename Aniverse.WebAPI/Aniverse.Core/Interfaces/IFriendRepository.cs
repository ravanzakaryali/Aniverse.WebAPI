using Aniverse.Core.Entites;
using Aniverse.Core.Entites.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.Core.Interfaces
{
    public interface IFriendRepository : IRepository<UserFriend>
    {
        public Task<List<string>> GetFriendId(string id, FriendRequestStatus enumStatus = FriendRequestStatus.Accepted);

    }
}
