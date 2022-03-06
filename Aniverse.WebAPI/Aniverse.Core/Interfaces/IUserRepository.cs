using Aniverse.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aniverse.Core.Interfaces
{
    public interface IUserRepository : IRepository<AppUser>
    {
        //Task<List<AppUser>> GetFriends(string username);
    }
}
