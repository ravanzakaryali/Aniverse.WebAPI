using Aniverse.Business.DTO_s.Friend;
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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Linq;

namespace Aniverse.Business.Implementations
{
    public class PostService : IPostService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        private readonly IHostEnvironment _hostEnvironment;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        public async Task CreateAsync(PostCreateDto postCreate, ClaimsPrincipal claims)
        {
            postCreate.Pictures = new List<PostImageDto>();
            var id = claims.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            postCreate.UserId = id;
            foreach (var picture in postCreate.ImageFile)
            {
                var image = new PostImageDto
                {
                    UserId = id,
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
                foreach (var item in post.Pictures)
                {
                    item.ImageName = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, item.ImageName);
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
                foreach (var item in post.Pictures)
                {
                    item.ImageName = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, item.ImageName);
                }
            }
            return posts;
        }
        public async Task<List<PostGetDto>> GetFriendPost(ClaimsPrincipal user, HttpRequest request, int page = 1, int size = 4)
        {
            var id = user.Identities.FirstOrDefault().Claims.FirstOrDefault().Value;
            var friends = await _unitOfWork.FriendRepository.GetFriendId(id);
            var posts = await _unitOfWork.PostRepository.GetAllPaginateAsync(page, size, p => friends.Contains(p.UserId) || p.UserId == id, "User","Comments","Likes","Animal","Pictures");
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p=>posts.Contains(p.Post));
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, picture.ImageName);
            }
            return _mapper.Map<List<PostGetDto>>(posts);
        }

    }
}
