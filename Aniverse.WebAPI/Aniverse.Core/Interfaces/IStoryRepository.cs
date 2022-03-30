using Aniverse.Core.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.Core.Interfaces
{
    public interface IStoryRepository : IRepository<Story>
    {
        Task<Story> CreateStory(Story story);
        Task<bool> OneStoryPerDay(string loginUserId);
    }
}
