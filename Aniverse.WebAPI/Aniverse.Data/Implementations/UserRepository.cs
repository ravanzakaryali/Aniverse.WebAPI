using Aniverse.Core.Entites;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aniverse.Data.Implementations
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private AppDbContext _context { get;}
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
