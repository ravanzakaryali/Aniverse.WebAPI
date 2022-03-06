using Aniverse.Core.Entites;
using System.Collections.Generic;

namespace Aniverse.Business.Services.Interface
{
    public interface IJwtService
    {
        public string GetJwt(AppUser user, IList<string> roles);
    }
}
