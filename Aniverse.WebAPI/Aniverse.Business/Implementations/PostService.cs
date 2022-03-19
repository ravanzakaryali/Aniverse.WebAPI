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
using System.Linq;
using Aniverse.Business.DTO_s.Comment;
using Aniverse.Core.Entities;
using Aniverse.Core.Entites.Enum;

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
                    AnimalId = postCreate.AnimalId,
                    ImageName = await picture.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                };
                postCreate.Pictures.Add(image);
            }
            await _unitOfWork.PostRepository.CreateAsync(_mapper.Map<Post>(postCreate));
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<PostGetDto>> GetAllAsync(HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var posts = await _unitOfWork.PostRepository.GetAllAsync(p=>!p.IsDelete && !p.IsArchive, "User", "Likes","Animal");
            var postsIds = posts.Select(f => f.Id);
            var userIds = posts.Select(p => p.UserId);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => posts.Contains(p.Post) || userIds.Contains(p.UserId));
            PictureDbName(pictures, request);
            var postMap = _mapper.Map<List<PostGetDto>>(posts);
            var comments = _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c => postsIds.Contains(c.PostId), "User"));
            var postSave = await _unitOfWork.SavePostRepository.GetAllAsync(p => p.UserId == userLoginId);
            var postSaveIds = postSave.Select(p => p.PostId);
            PostUserProfilePicture(postMap, postSaveIds, comments, pictures);
            CommentUserProfilePicture(pictures, comments);
            return postMap;
        }
        public async Task<List<PostGetDto>> GetAsync(string id, HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var posts = await _unitOfWork.PostRepository.GetAllAsync(p => p.User.UserName == id && !p.IsArchive && !p.IsDelete, "User", "Likes", "Pictures");
            var postsIds = posts.Select(f => f.Id);
            var userIds = posts.Select(p => p.UserId);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => posts.Contains(p.Post) || userIds.Contains(p.UserId));
            PictureDbName(pictures, request);
            var postMap = _mapper.Map<List<PostGetDto>>(posts);
            var comments = _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c => postsIds.Contains(c.PostId), "User"));
            var postSave = await _unitOfWork.SavePostRepository.GetAllAsync(p => p.UserId == userLoginId);
            var postSaveIds = postSave.Select(p => p.PostId);
            PostUserProfilePicture(postMap, postSaveIds, comments, pictures);
            CommentUserProfilePicture(pictures, comments);
            return postMap;
        }
        public async Task<List<PostGetDto>> GetAnimalPosts(string animalname, HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var animalPost = await _unitOfWork.PostRepository.GetAllAsync(p => p.Animal.Animalname == animalname, "User", "Likes", "Comments", "Comments.User", "Pictures", "Animal");
            if (animalPost is null)
            {
                throw new NotFoundException("Animal post not found");
            }
            var postsIds = animalPost.Select(f => f.Id);
            var userIds = animalPost.Select(p => p.UserId);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => animalPost.Contains(p.Post) || userIds.Contains(p.UserId));
            PictureDbName(pictures, request);
            var postMap = _mapper.Map<List<PostGetDto>>(animalPost);
            var comments = _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c => postsIds.Contains(c.PostId), "User"));
            var postSave = await _unitOfWork.SavePostRepository.GetAllAsync(p => p.UserId == userLoginId);
            var postSaveIds = postSave.Select(p => p.PostId);
            PostUserProfilePicture(postMap, postSaveIds, comments, pictures);
            CommentUserProfilePicture(pictures, comments);
            return postMap;
        }
        public async Task<List<PostGetDto>> GetAllArchive(HttpRequest request, int page, int size)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var posts = await _unitOfWork.PostRepository.GetAllPaginateAsync(page,size,p=>p.CreationDate,p => p.UserId == userLoginId && p.IsArchive == true, "User", "Pictures", "Animal");
            if (posts is null)
            {
                throw new NotFoundException("Animal post not found");
            }
            var postsIds = posts.Select(f => f.Id);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => posts.Contains(p.Post));
            PictureDbName(pictures, request);
            return _mapper.Map<List<PostGetDto>>(posts);

        }
        public async Task<List<PostGetDto>> GetFriendPost(HttpRequest request, int page = 1, int size = 4)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var friends = await _unitOfWork.FriendRepository.GetAllAsync(f => f.UserId == userLoginId || f.FriendId == userLoginId && f.Status == FriendRequestStatus.Accepted);

            if (friends is null)
            {
                throw new NotFoundException("Friends is null");
            }
            var userIds = friends.Select(f => f.UserId);
            var friendsId = friends.Select(f => f.FriendId);
            var posts = await _unitOfWork.PostRepository.GetAllPaginateAsync(page, size,p=>p.Id ,p => !p.IsArchive && !p.IsDelete && (friendsId.Contains(p.UserId) || userIds.Contains(p.UserId)), "User", "Likes", "Animal");
            var postsIds = posts.Select(f => f.Id);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => posts.Contains(p.Post) || friendsId.Contains(p.UserId) || p.UserId == userLoginId);
            PictureDbName(pictures,request);
            var postMap = _mapper.Map<List<PostGetDto>>(posts);
            var comments = _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c => postsIds.Contains(c.PostId), "User"));
            var postSave = await _unitOfWork.SavePostRepository.GetAllAsync(p => p.UserId == userLoginId);
            var postSaveIds = postSave.Select(p => p.PostId);
            PostUserProfilePicture(postMap,postSaveIds,comments,pictures);
            CommentUserProfilePicture(pictures, comments);
            return postMap;
        }
        public async Task PostSaveAsync(PostSaveDto postSave)
        {
            var postDb = await _unitOfWork.PostRepository.GetAsync(p => p.Id == postSave.PostId);
            if (postDb is null)
            {
                throw new NotFoundException("Post is not found");
            };
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            if (postSave.IsSave)
            {
                var savePost = new SavePost
                {
                    PostId = postSave.PostId,
                    UserId = userLoginId
                };
                await _unitOfWork.SavePostRepository.CreateAsync(savePost);
            }
            else
            {
                SavePost posSaveDb = await _unitOfWork.SavePostRepository.GetAsync(s => s.UserId == userLoginId && s.PostId == postSave.PostId);
                if (posSaveDb is null)
                {
                    throw new NotFoundException("Post is not found");
                }
                _unitOfWork.SavePostRepository.Delete(posSaveDb);
            }
            await _unitOfWork.SaveAsync();
        }
        public async Task PostUpdateAsync(int id, PostCreateDto postCreate)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var postDb = await _unitOfWork.PostRepository.GetAsync(p => p.Id == id && p.UserId == userLoginId);
            if (postDb is null)
            {
                throw new NotFoundException("Post is not found");
            };
            postDb.IsModified = true;
            postDb.Content = postCreate.Content;
            _unitOfWork.PostRepository.Update(postDb);
            await _unitOfWork.SaveAsync();
        }
        public async Task PostDeleteAsync(int id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var postDb = await _unitOfWork.PostRepository.GetAsync(p => p.Id == id && p.UserId == userLoginId);
            if (postDb is null)
            {
                throw new NotFoundException("Post is not found");
            };
            postDb.IsDelete =true;
            await _unitOfWork.SaveAsync();
        }
        private void CommentUserProfilePicture(List<Picture> pictures, List<CommentGetDto> comments)
        {
            foreach (var comment in comments)
            {
                if (pictures.Any(p => p.UserId == comment.UserId && p.IsProfilePicture))
                    comment.User.ProfilPicture = pictures.Where(p => p.UserId == comment.UserId && p.IsProfilePicture).FirstOrDefault().ImageName;
            }
        }
        private void PictureDbName(List<Picture> pictures, HttpRequest request)
        {
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
        }
        private void PostUserProfilePicture(List<PostGetDto> postMap, IEnumerable<int> postSaveIds,List<CommentGetDto> comments, List<Picture> pictures)
        {
            foreach (var post in postMap)
            {
                if (postSaveIds.Contains(post.Id))
                {
                    post.IsSave = true;
                }
                post.Comments = comments.Where(c => c.PostId == post.Id).ToList();
                if (pictures.Any(p => p.UserId == post.UserId && p.IsProfilePicture))
                    post.User.ProfilPicture = pictures.Where(p => p.UserId == post.UserId && p.IsProfilePicture).FirstOrDefault().ImageName;
            }
        }
    }
}
