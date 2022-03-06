using Aniverse.Business.DTO_s.Animal;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.Exceptions;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aniverse.Business.Implementations
{
    public class AnimalService : IAnimalService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public AnimalService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AnimalGetDto> GetAsync(string id)
        {

            AnimalGetDto animal = _mapper.Map<AnimalGetDto>(await _unitOfWork.AnimalRepository.GetAsync(a => a.Animalname == id, "User"));
            var PostCount = await _unitOfWork.PostRepository.GetAllAsync(p => p.Animal.Animalname == id);
            List<AnimalFollow> follow = await _unitOfWork.AnimalFollowRepository.GetAllAsync(a => a.Animal.Animalname == id, "User");
            animal.AnimalFollow = _mapper.Map<List<AnimalFollowDto>>(follow);
            animal.PostCount = PostCount.Count;
            return animal;

        }
        public async Task<List<AnimalAllDto>> GetAllAsync()
        {
            return _mapper.Map<List<AnimalAllDto>>(await _unitOfWork.AnimalRepository.GetAllAsync());
        }
        public async Task<List<AnimalAllDto>> GetFriendAsync(string username)
        {
            return _mapper.Map<List<AnimalAllDto>>(await _unitOfWork.AnimalRepository.GetFriendAnimals(username));
        }
        public async Task<List<AnimalAllDto>> GetAnimalUserAsync(string username)
        {
            return _mapper.Map<List<AnimalAllDto>>(await _unitOfWork.AnimalRepository.GetAllAsync(a => a.User.UserName == username, "User"));
        }
        public async Task<List<PostGetDto>> GetAnimalPosts(string animalname, HttpRequest request)
        {
            var posts = _mapper.Map<List<PostGetDto>>(await _unitOfWork.PostRepository.GetAllAsync(p => p.Animal.Animalname == animalname, "User", "Likes", "Comments", "Comments.User", "Pictures", "Animal"));
            foreach (var post in posts)
            {
                foreach (var item in post.Pictures)
                {
                    item.ImageName = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, item.ImageName);
                    var name = item.ImageName;
                }
            }
            return posts;
        }

        public async Task FollowCreate(FollowDto follow, ClaimsPrincipal user)
        {

            var UserId = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            if (follow.IsFollowing)
            {
                var followEntity = new AnimalFollow
                {
                    AnimalId = follow.AnimalId,
                    UserId = UserId,
                };
                await _unitOfWork.AnimalFollowRepository.CreateAsync(followEntity);
            }
            else
            {
                var followDelete = await _unitOfWork.AnimalFollowRepository.GetAsync(a => a.UserId == UserId && a.AnimalId == follow.AnimalId);
                _unitOfWork.AnimalFollowRepository.Delete(followDelete);
            }
            await _unitOfWork.SaveAsync();
        }
        public async Task<List<AnimalGetCategory>> GetAnimalCategory()
        {
            return _mapper.Map<List<AnimalGetCategory>>(await _unitOfWork.AnimalCategory.GetAllAsync());
        }

        public async Task AnimalCreateAsync(AnimalCreateDto animalCreate, ClaimsPrincipal user)
        {
            var animalnameDb = await _unitOfWork.AnimalRepository.GetAsync(a => a.Animalname == animalCreate.Animalname);
            if (animalnameDb != null)
            {
                throw new AlreadyException("Already animalname");
            }
            animalCreate.UserId = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            var animal = _mapper.Map<Animal>(animalCreate);
            await _unitOfWork.AnimalRepository.CreateAsync(animal);
            await _unitOfWork.SaveAsync();
        }
        public async Task<List<AnimalSelectGetDto>> SelectAnimal(ClaimsPrincipal user)
        {
            var UserId = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            return _mapper.Map<List<AnimalSelectGetDto>>(await _unitOfWork.AnimalRepository.GetAllAsync(a => a.UserId == UserId));
        }

        public async Task UpdateAnimalAsync(int id, AnimalUpdateDto animalUpdate, ClaimsPrincipal user)
        {
            var UserId = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            var animal = await _unitOfWork.AnimalRepository.GetAsync(a => a.Id == id && a.UserId == UserId);
            if (animal is null)
            {
                throw new NotFoundException("Not found animal");
            }
            animal.Name = animalUpdate.Name;
            animal.Bio = animalUpdate.Bio;
            animal.Breed = animalUpdate.Breed;
            _unitOfWork.AnimalRepository.Update(animal);
            await _unitOfWork.SaveAsync();
        }
    }
}
