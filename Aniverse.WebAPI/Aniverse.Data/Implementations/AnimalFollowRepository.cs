using Aniverse.Core.Entites;
using Aniverse.Core.Interfaces;
using Aniverse.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Data.Implementations
{
    public class AnimalFollowRepository : Repository<AnimalFollow>, IAnimalFollowRepository
    {
        private readonly AppDbContext _contetx;
        public AnimalFollowRepository(AppDbContext context) : base(context)
        {
            _contetx = context;
        }
    }
}
