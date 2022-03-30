using Aniverse.Core.Entites;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Aniverse.Data.Implementations
{
    public class StoryRepository : Repository<Story>, IStoryRepository
    {
        private readonly AppDbContext _context;
        public StoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Story> CreateStory(Story story)
        {
            await _context.Story.AddAsync(story);
            await _context.SaveChangesAsync();
            return await _context.Story.Where(s => s.Id == story.Id).Include(s=>s.User).FirstOrDefaultAsync();
        }

        public async Task<bool> OneStoryPerDay(string loginUserId)
        {
            var dateNow = DateTime.Today.DayOfYear;
            var story = await _context.Story.Where(s=>s.UserId == loginUserId && !s.IsDeleted && !s.IsArchive).FirstOrDefaultAsync();
            if(story is null)
            {
                return true;
            }
            if(story.CreatedDate.DayOfYear == dateNow)
            {
                return false;
            }
            return true;
            
        }
    }
}
