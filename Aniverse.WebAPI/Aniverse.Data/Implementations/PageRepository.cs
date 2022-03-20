using Aniverse.Core.Entities;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Data.Implementations
{
    public class PageRepository : Repository<Page>, IPageRepository
    {
        private readonly AppDbContext _context;
        public PageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
