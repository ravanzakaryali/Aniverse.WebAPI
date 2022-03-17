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
        Task<List<UserAllDto>> GetAllAsync();
        Task<UserGetDto> GetAsync(string id, HttpRequest request);
        Task ChangeBio(JsonPatchDocument<AppUser> bioChange);
        Task ProfileCreate(ProfileCreateDto profilCreate);
        Task CoverCreate(ProfileCreateDto profilCreate);
        Task<List<UserGetDto>> GetBlcokUsersAsync();
        Task<UserGetDto> GetLoginUser();
        Task<List<GetPictureDto>> GetPhotos(string username, HttpRequest request, int page, int size);
        Task<List<GetPictureDto>> GetUserPhotos(string username, HttpRequest request, int page, int size);
        Task<List<UserAllDto>> SearchAsync(string search);
    }
}
