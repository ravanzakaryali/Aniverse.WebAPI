using Aniverse.Business.DTO_s.Animal;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.Exceptions;
using Aniverse.Business.Extensions;
using Aniverse.Business.Helpers;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using Aniverse.Core.Entites.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
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
        private readonly IHostEnvironment _hostEnvironment;


        public AnimalService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<AnimalGetDto> GetAsync(string id, HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
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
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => p.Animal.Animalname == id && (p.IsAnimalCoverPicture == true|| p.IsAnimalProfilePicture));
            
            foreach (var picture in pictures)
            {
                if (picture.IsAnimalProfilePicture)
                {
                    animal.ProfilPicture = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
                }
                if (picture.IsAnimalCoverPicture)
                {
                    animal.CoverPicture = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");

                }
            }

            animal.AnimalFollow = _mapper.Map<List<AnimalFollowDto>>(follow);
            animal.PostCount = PostCount.Count;
            animal.IsFollow = animal.AnimalFollow.Any(a=>a.User.Id == userLoginId);
            return animal;
        }
        public async Task<List<AnimalAllDto>> GetAllAsync(HttpRequest request,int page = 1, int size = 3)
        {
            var animals = _mapper.Map<List<AnimalAllDto>>(await _unitOfWork.AnimalRepository.GetAllPaginateAsync(page, size, a => a.Id));
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => p.IsAnimalCoverPicture == true || p.IsAnimalProfilePicture);

            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            foreach (var animal in animals)
            {
                if (pictures.Any(p => animal.Id == p.AnimalId && p.IsAnimalProfilePicture))
                    animal.ProfilePicture = pictures.Where(p => animal.Id == p.AnimalId && p.IsAnimalProfilePicture).FirstOrDefault().ImageName;
            }
            return animals;
        }
        public async Task<List<AnimalAllDto>> GetFriendAnimals(HttpRequest request,string username, int page=1, int size=3)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var friends = await _unitOfWork.FriendRepository.GetAllAsync(u => (u.User.UserName == username || u.Friend.UserName == username) && u.Status == FriendRequestStatus.Accepted);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => p.IsAnimalCoverPicture == true || p.IsAnimalProfilePicture);
            if (friends is null)
            {
                throw new NotFoundException("Friend is not found");
            }
            var friendIds = friends.Select(x => x.FriendId);
            var userIds = friends.Select(x => x.UserId);
            var animals = _mapper.Map<List<AnimalAllDto>>( await _unitOfWork.AnimalRepository.GetAllPaginateAsync(page, size, a => a.Birthday, a => friendIds.Contains(a.UserId) || userIds.Contains(a.UserId)));
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            foreach (var animal in animals)
            {
                if (pictures.Any(p => animal.Id == p.AnimalId && p.IsAnimalProfilePicture))
                    animal.ProfilePicture = pictures.Where(p => animal.Id == p.AnimalId && p.IsAnimalProfilePicture).FirstOrDefault().ImageName;
            }
            return animals;
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
            
            return posts;
        }
        public async Task FollowCreate(int id,FollowDto follow)
        {

            var UserLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            if (follow.IsFollowing)
            {
                var followEntity = new AnimalFollow
                {
                    AnimalId = id,
                    UserId = UserLoginId,
                };
                await _unitOfWork.AnimalFollowRepository.CreateAsync(followEntity);
            }
            else
            {
                var followDelete = await _unitOfWork.AnimalFollowRepository.GetAsync(a => a.UserId == UserLoginId && a.AnimalId == id);
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
            var animalCategory = await _unitOfWork.AnimalCategoryRepository.GetAllAsync();
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
        public async Task<List<AnimalAllDto>> AnimalUserFollows(string username)
        {
            var userDb = await _unitOfWork.UserRepository.GetAsync(u=>u.UserName == username);
            if(userDb is null)
            {
                throw new Exception();
            }
            var animalFollow = await _unitOfWork.AnimalFollowRepository.GetAllAsync(a=>a.UserId == userDb.Id, "Animal");
            return _mapper.Map<List<AnimalAllDto>>(animalFollow.Select(a=>a.Animal).ToList());   
        }
        public async Task<List<GetPictureDto>> GetAnimalPhotos(string animalname, HttpRequest request, int page = 1, int size = 1)
        {
            var photos = await _unitOfWork.PictureRepository.GetAllPaginateAsync(page , size,p=>p.Id, p => p.Animal.Animalname == animalname);
            var photosMap = _mapper.Map<List<GetPictureDto>>(photos);

            for (int i = 0; i < photosMap.Count; i++)
            {
                photosMap[i].ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{photos[i].ImageName}");

            }
            return photosMap;
        }
        public async Task ChangeCoverPicture(int id, AnimalPictureChangeDto coverCreate)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var picture = new Picture
            {
                IsAnimalCoverPicture = true,
                AnimalId = id,
                ImageName = await coverCreate.ImageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                UserId = userLoginId,
            };
            var coverPictureDb = await _unitOfWork.PictureRepository.GetAsync(p => p.UserId == userLoginId && p.AnimalId == id && p.IsAnimalCoverPicture == true);
            await _unitOfWork.PictureRepository.CreateAsync(picture);
            if (coverPictureDb != null)
            {
                coverPictureDb.IsAnimalCoverPicture = false;
            }
            await _unitOfWork.SaveAsync();
        }
        public async Task ChangeProfilePicture(int id, AnimalPictureChangeDto coverCreate)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var picture = new Picture
            {
                IsAnimalProfilePicture = true,
                AnimalId = id,
                ImageName = await coverCreate.ImageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                UserId = userLoginId,
            };
            var coverPictureDb = await _unitOfWork.PictureRepository.GetAsync(p => p.UserId == userLoginId && p.AnimalId == id && p.IsAnimalProfilePicture == true);
            await _unitOfWork.PictureRepository.CreateAsync(picture);
            if (coverPictureDb != null)
            {
                coverPictureDb.IsAnimalProfilePicture = false;
            }
            await _unitOfWork.SaveAsync();
        }
    }
}
