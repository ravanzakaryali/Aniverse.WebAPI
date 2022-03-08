using Aniverse.Core.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.Core.Interfaces
{
    public interface IStoryRepository : IRepository<Story>
    {
        //public Task<List<Story>> GetFriendStory(string id);
    }
}
