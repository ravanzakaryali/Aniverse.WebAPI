using Aniverse.Business.DTO_s.Animal;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface IAnimalService
    {
        Task<AnimalGetDto> GetAsync(string id);
        Task<List<AnimalAllDto>> GetAllAsync();
        Task<List<AnimalAllDto>> GetFriendAnimals(string username, int page, int size);
        Task<List<AnimalAllDto>> GetAnimalUserAsync(string username);
        Task<List<PostGetDto>> GetAnimalPosts(string animalname, HttpRequest request);
        Task FollowCreate(FollowDto follow);
        Task<List<AnimalGetCategory>> GetAnimalCategory();
        Task AnimalCreateAsync(AnimalCreateDto animalCreate);
        Task<List<AnimalSelectGetDto>> SelectAnimal();
        Task UpdateAnimalAsync(int id, AnimalUpdateDto animalUpdate);
        Task<List<AnimalAllDto>> AnimalUserFollows(string username);
        Task<List<GetPictureDto>> GetAnimalPhotos(string animalname, HttpRequest request, int page, int size);
        Task ChangeCoverPicture(int id, AnimalPictureChangeDto cover);
        Task ChangeProfilePicture(int id, AnimalPictureChangeDto profile);
    }
}
