using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.Interface;
using Aniverse.Core;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aniverse.Business.Helpers;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.Http;
using System;
using Aniverse.Business.Extensions;
using Aniverse.Business.Exceptions;

namespace Aniverse.Business.Implementations
{
    public class PostService : IPostService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(PostCreateDto postCreate)
        {
            postCreate.Pictures = new List<PostImageDto>();
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            postCreate.UserId = userLoginId;
            foreach (var picture in postCreate.ImageFile)
            {
                var image = new PostImageDto
                {
                    UserId = userLoginId,
                    ImageName = await picture.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                };
                postCreate.Pictures.Add(image);
            }
            await _unitOfWork.PostRepository.CreateAsync(_mapper.Map<Post>(postCreate));
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<PostGetDto>> GetAllAsync(HttpRequest request)
        {
            var posts = _mapper.Map<List<PostGetDto>>(await _unitOfWork.PostRepository.GetAllAsync(null, "Comments", "User", "Likes"));
            foreach (var post in posts)
            {
                foreach (var picture in post.Pictures)
                {
                    picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
                }
            }
            return posts;
        }
        public async Task<List<PostGetDto>> GetAllAsync(string id)
        {
            var user = _mapper.Map<List<PostGetDto>>(await _unitOfWork.FriendRepository.GetAllAsync(u => u.User.UserName == id, "Friend"));
            return user;
        }
        public async Task<List<PostGetDto>> GetAsync(string id, HttpRequest request)
        {
            var posts = _mapper.Map<List<PostGetDto>>(await _unitOfWork.PostRepository.GetAllAsync(p => p.User.UserName == id, "Comments", "Comments.User", "User", "Likes", "Pictures"));
            foreach (var post in posts)
            {
                foreach (var picture in post.Pictures)
                {
                    picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
                }
            }
            return posts;
        }
        public async Task<List<PostGetDto>> GetFriendPost(HttpRequest request, int page = 1, int size = 4)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var friends = await _unitOfWork.FriendRepository.GetFriendId(userLoginId);
            if(friends is null)
            {
                throw new NotFoundException("Friends is null");
            }
            var posts = await _unitOfWork.PostRepository.GetAllPaginateAsync(page, size, p => friends.Contains(p.UserId) || p.UserId == userLoginId, "User","Comments","Likes","Animal","Pictures");
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p=>posts.Contains(p.Post));
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            return _mapper.Map<List<PostGetDto>>(posts);
        }

    }
}
