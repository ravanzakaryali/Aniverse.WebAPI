using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.User;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface IUserService
    {
        Task<List<UserAllDto>> GetAllAsync(ClaimsPrincipal user );
        Task<UserGetDto> GetAsync(string id, HttpRequest request);
        Task ChangeBio(JsonPatchDocument<AppUser> bioChange, ClaimsPrincipal user);
        Task ProfileCreate(ProfileCreateDto profilCreate, ClaimsPrincipal user);
        Task CoverCreate(ProfileCreateDto profilCreate, ClaimsPrincipal user);
        Task<List<GetPictureDto>> GetPhotos(string username, HttpRequest request, int page, int size);
        Task<List<UserGetDto>> GetBlcokUsersAsync(ClaimsPrincipal user);
    }
}
