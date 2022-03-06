using Aniverse.Core.Entites;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Data.Implementations
{
    public class PictureRepository : Repository<Picture>, IPictureRepository
    {
        private AppDbContext _context;
        public PictureRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
