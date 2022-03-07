using Aniverse.Business.DTO_s.Animal;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.Exceptions;
using Aniverse.Business.Extensions;
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
        public readonly IHttpContextAccessor _httpContextAccessor;

        public AnimalService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<AnimalGetDto> GetAsync(string id)
        {
            var animals = await _unitOfWork.AnimalRepository.GetAsync(a => a.Animalname == id, "User");
            if(animals is null)
            {
                throw new NotFoundException("Animals is not found");
            }
            AnimalGetDto animal = _mapper.Map<AnimalGetDto>(animals);
            var PostCount = await _unitOfWork.PostRepository.GetAllAsync(p => p.Animal.Animalname == id);

            List<AnimalFollow> follow = await _unitOfWork.AnimalFollowRepository.GetAllAsync(a => a.Animal.Animalname == id, "User");

            if(follow is null)
            {
                throw new NotFoundException("Follow is not found");
            }

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
            var animalPost = await _unitOfWork.PostRepository.GetAllAsync(p => p.Animal.Animalname == animalname, "User", "Likes", "Comments", "Comments.User", "Pictures", "Animal");
            if(animalPost is null)
            {
                throw new NotFoundException("Animal post not found");
            }
            var posts = _mapper.Map<List<PostGetDto>>(animalPost);
            foreach (var post in posts)
            {
                foreach (var item in post.Pictures)
                {
                    item.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{item.ImageName}");
                    var name = item.ImageName;
                }
            }
            return posts;
        }

        public async Task FollowCreate(FollowDto follow)
        {

            var UserLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            if (follow.IsFollowing)
            {
                var followEntity = new AnimalFollow
                {
                    AnimalId = follow.AnimalId,
                    UserId = UserLoginId,
                };
                await _unitOfWork.AnimalFollowRepository.CreateAsync(followEntity);
            }
            else
            {
                var followDelete = await _unitOfWork.AnimalFollowRepository.GetAsync(a => a.UserId == UserLoginId && a.AnimalId == follow.AnimalId);
                if(followDelete is null)
                {
                    throw new NotFoundException("Follow is not found");
                }
                _unitOfWork.AnimalFollowRepository.Delete(followDelete);
            }
            await _unitOfWork.SaveAsync();
        }
        public async Task<List<AnimalGetCategory>> GetAnimalCategory()
        {
            var animalCategory = await _unitOfWork.AnimalCategory.GetAllAsync();
            if(animalCategory is null)
            {
                throw new NotFoundException("Animal category is not found");
            }
            return _mapper.Map<List<AnimalGetCategory>>(animalCategory);
        }

        public async Task AnimalCreateAsync(AnimalCreateDto animalCreate)
        {
            var animalnameDb = await _unitOfWork.AnimalRepository.GetAsync(a => a.Animalname == animalCreate.Animalname);
            if (animalnameDb != null)
            {
                throw new AlreadyException("Already animalname");
            }
            animalCreate.UserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var animal = _mapper.Map<Animal>(animalCreate);
            await _unitOfWork.AnimalRepository.CreateAsync(animal);
            await _unitOfWork.SaveAsync();
        }
        public async Task<List<AnimalSelectGetDto>> SelectAnimal()
        {
            var UserId = _httpContextAccessor.HttpContext.User.GetUserId();
            return _mapper.Map<List<AnimalSelectGetDto>>(await _unitOfWork.AnimalRepository.GetAllAsync(a => a.UserId == UserId));
        }

        public async Task UpdateAnimalAsync(int id, AnimalUpdateDto animalUpdate)
        {
            var UserLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var animal = await _unitOfWork.AnimalRepository.GetAsync(a => a.Id == id && a.UserId == UserLoginId);
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
