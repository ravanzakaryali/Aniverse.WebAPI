using Aniverse.Core.Entites;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;

namespace Aniverse.Data.Implementations
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        private AppDbContext _context;
        public AnimalRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        //public async Task<List<Animal>> GetFriendAnimals(string username)
        //{
        //    var friends = await _context.UserFriends.Where(u => u.User.UserName == username && u.Status == FriendRequestStatus.Accepted).Select(u => u.FriendId).Distinct().ToListAsync();
        //    var animals = await _context.Animal
        //        .Where(p => friends
        //        .Contains(p.UserId))
        //        .Include(p => p.User)
        //        .ToListAsync();
        //    return animals;
        //}
    }
}
