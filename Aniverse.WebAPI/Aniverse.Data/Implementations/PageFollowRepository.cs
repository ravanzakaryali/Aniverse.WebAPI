using Aniverse.Core.Entities;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Data.Implementations
{
    public class PageFollowRepository : Repository<PageFollow>, IPageFollowRepository
    {
        private AppDbContext _context;
        public PageFollowRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
