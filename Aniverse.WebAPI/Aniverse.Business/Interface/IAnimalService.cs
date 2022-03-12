using Aniverse.Business.DTO_s.Animal;
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
        Task<List<AnimalAllDto>> GetFriendAsync(string username);
        Task<List<AnimalAllDto>> GetAnimalUserAsync(string username);
        Task<List<PostGetDto>> GetAnimalPosts(string animalname, HttpRequest request);
        Task FollowCreate(FollowDto follow);
        Task<List<AnimalGetCategory>> GetAnimalCategory();
        Task AnimalCreateAsync(AnimalCreateDto animalCreate);
        Task<List<AnimalSelectGetDto>> SelectAnimal();
        Task UpdateAnimalAsync(int id, AnimalUpdateDto animalUpdate);
        Task<List<AnimalAllDto>> AnimalUserFollows(string username);
    }
}
