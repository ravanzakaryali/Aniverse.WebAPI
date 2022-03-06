using Aniverse.Core.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.Core.Interfaces
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        public Task<List<Animal>> GetFriendAnimals(string username);
    }
}
