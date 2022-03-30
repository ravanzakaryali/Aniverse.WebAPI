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
using Aniverse.Business.Exceptions.FileExceptions;

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

        public async Task<PostGetDto> CreateAsync(HttpRequest request, PostCreateDto postCreate)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            postCreate.UserId = userLoginId;
            if (postCreate.ImageFile != null)
            {
                foreach (var file in postCreate.ImageFile)
                {
                    if (!file.CheckFileSize(10000))
                        throw new FileTypeException("File max size 100 mb");
                    if (!file.CheckFileType("image/"))
                        throw new FileSizeException("File type must be image");
                }
                postCreate.Pictures = new List<PostImageDto>();
                foreach (var picture in postCreate.ImageFile)
                {
                    var image = new PostImageDto
                    {
                        UserId = userLoginId,
                        AnimalId = postCreate.AnimalId,
                        PageId = postCreate.PageId,
                        ImageName = await picture.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                    };
                    postCreate.Pictures.Add(image);
                }
            }
            var newPost = await _unitOfWork.PostRepository.CreatePost(_mapper.Map<Post>(postCreate));
            await _unitOfWork.SaveAsync();
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => newPost.Id == p.PostId || userLoginId == p.UserId);
            PictureDbName(pictures, request);
            var postMap = _mapper.Map<PostGetDto>(newPost);
            PostUserProfile(postMap, pictures);
            return postMap;
        }

        public async Task<List<PostGetDto>> GetAllAsync(HttpRequest request, int page, int size)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var posts = await _unitOfWork.PostRepository.GetAllPaginateAsync(page, size, p => p.CreationDate, p => !p.IsDelete && !p.IsArchive && p.PageId == null, "User", "Likes", "Animal");
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
        public async Task<List<PostGetDto>> GetAsync(string id,int page,int size, HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var posts = await _unitOfWork.PostRepository.GetAllPaginateAsync(page,size,p=>p.CreationDate,p => p.User.UserName == id && !p.IsArchive && !p.IsDelete && p.PageId == null, "User", "Likes","Animal");
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
            var animalPost = await _unitOfWork.PostRepository.GetAllAsync(p => p.Animal.Animalname == animalname && p.PageId == null, "User", "Likes", "Comments", "Comments.User", "Animal");
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
            var posts = await _unitOfWork.PostRepository.GetAllPaginateAsync(page, size, p => p.CreationDate, p => p.UserId == userLoginId && p.IsArchive == true, "User", "Likes", "Comments", "Comments.User", "Animal");
            if (posts is null)
            {
                throw new NotFoundException("Post is not found");
            }
            var postsIds = posts.Select(f => f.Id);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => posts.Contains(p.Post) || p.UserId == userLoginId);
            PictureDbName(pictures, request);
            var postMap = _mapper.Map<List<PostGetDto>>(posts);
            var comments = _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c => postsIds.Contains(c.PostId), "User"));
            var postSave = await _unitOfWork.SavePostRepository.GetAllAsync(p => p.UserId == userLoginId);
            var postSaveIds = postSave.Select(p => p.PostId);
            PostUserProfilePicture(postMap, postSaveIds, comments, pictures);
            CommentUserProfilePicture(pictures, comments);
            return postMap;

        }
        public async Task<List<PostGetDto>> GetAllRecycle(HttpRequest request, int page, int size)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var posts = await _unitOfWork.PostRepository.GetAllPaginateAsync(page, size, p => p.CreationDate, p => p.UserId == userLoginId && p.IsDelete == true, "User", "Likes", "Comments", "Comments.User", "Animal");
            if (posts is null)
            {
                throw new NotFoundException("Animal post not found");
            }
            var postIds = posts.Select(f => f.Id);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => posts.Contains(p.Post) || p.UserId == userLoginId);
            PictureDbName(pictures, request);
            var postMap = _mapper.Map<List<PostGetDto>>(posts);
            var comments = _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c => postIds.Contains(c.PostId), "User"));
            var postSave = await _unitOfWork.SavePostRepository.GetAllAsync(p => p.UserId == userLoginId);
            var postSaveIds = postSave.Select(p => p.PostId);
            PostUserProfilePicture(postMap, postSaveIds, comments, pictures);
            CommentUserProfilePicture(pictures, comments);
            return postMap;

        }
        public async Task<List<PostGetDto>> GetAllSave(HttpRequest request, int page, int size)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var postS = await _unitOfWork.SavePostRepository.GetAllAsync(p => p.UserId == userLoginId,"Post.User");
            var postIds = postS.Select(p => p.PostId);
            var postSelect = postS.Select(p => p.Post.UserId);
            var posts = await _unitOfWork.PostRepository.GetAllPaginateAsync(page, size, p => p.CreationDate, p => postIds.Contains(p.Id), "User", "Likes", "Comments", "Comments.User", "Animal");
            if (posts is null)
            {
                throw new NotFoundException("Animal post not found");
            }
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => ((postSelect.Contains(p.UserId)  || p.UserId == userLoginId) && p.IsProfilePicture ) || postIds.Contains((int)p.PostId));
            PictureDbName(pictures, request);
            var postMap = _mapper.Map<List<PostGetDto>>(posts);
            var comments = _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c => postIds.Contains(c.PostId), "User"));
            var postSave = await _unitOfWork.SavePostRepository.GetAllAsync(p => p.UserId == userLoginId);
            var postSaveIds = postSave.Select(p => p.PostId);
            PostUserProfilePicture(postMap, postSaveIds, comments, pictures);
            CommentUserProfilePicture(pictures, comments);
            return postMap;

        }
        public async Task<List<PostGetDto>> GetFriendPost(HttpRequest request, int page = 1, int size = 4)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var friends = await _unitOfWork.FriendRepository.GetAllAsync(f => (f.UserId == userLoginId || 
                                                                         f.FriendId == userLoginId) && 
                                                                         f.Status == FriendRequestStatus.Accepted);
            if (friends is null)
            {
                throw new NotFoundException("Friends is null");
            }
            var userIds = friends.Select(f => f.UserId);
            var friendsId = friends.Select(f => f.FriendId);
            var posts = await _unitOfWork.PostRepository.GetAllPaginateAsync(page, size, 
                                                                            p => p.Id, 
                                                                            p => !p.IsArchive && 
                                                                            !p.IsDelete && 
                                                                            p.PageId == null &&(friendsId.Contains(p.UserId) ||
                                                                            userIds.Contains(p.UserId)) || (p.UserId == userLoginId && p.PageId == null), 
                                                                            "User", "Likes", "Animal");
            var postsIds = posts.Select(f => f.Id);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => posts.Contains(p.Post) || userIds.Contains(p.UserId) || friendsId.Contains(p.UserId) || p.UserId == userLoginId);
            PictureDbName(pictures, request);
            var postMap = _mapper.Map<List<PostGetDto>>(posts);
            var comments = _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c => postsIds.Contains(c.PostId), "User"));
            var postSave = await _unitOfWork.SavePostRepository.GetAllAsync(p => p.UserId == userLoginId);
            var postSaveIds = postSave.Select(p => p.PostId);
            PostUserProfilePicture(postMap, postSaveIds, comments, pictures);
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
        public async Task PostUpdateAsync(int id, PostUpdateDto postUpdate)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var postDb = await _unitOfWork.PostRepository.GetAsync(p => p.Id == id && p.UserId == userLoginId);
            if (postDb is null)
            {
                throw new NotFoundException("Post is not found");
            };
            postDb.IsModified = true;
            postDb.Content = postUpdate.Content;
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
            postDb.IsDelete = true;
            await _unitOfWork.SaveAsync();
        }
        public async Task PostArchiveAsync(int id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var postDb = await _unitOfWork.PostRepository.GetAsync(p => p.Id == id && p.UserId == userLoginId);
            if (postDb is null)
            {
                throw new NotFoundException("Post is not found");
            };
            postDb.IsArchive = true;
            await _unitOfWork.SaveAsync();
        }
        public async Task PostDbDeleteAsync(int id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var postDb = await _unitOfWork.PostRepository.GetAsync(p => p.Id == id && p.UserId == userLoginId);
            if (postDb is null)
            {
                throw new NotFoundException("Post is not found");
            };
            _unitOfWork.PostRepository.Delete(postDb);
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
        public async Task PostReduceAsync(int id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var post = await _unitOfWork.PostRepository.GetAsync(p => p.Id == id && p.UserId == userLoginId);
            if(post is null)
            {
                throw new NotFoundException("Post is not found");
            }
            post.IsDelete = false;
            post.IsArchive = false;
            await _unitOfWork.SaveAsync();
        }
        private void PostUserProfilePicture(List<PostGetDto> postMap, IEnumerable<int> postSaveIds, List<CommentGetDto> comments, List<Picture> pictures)
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
        private void PostUserProfile(PostGetDto postMap, List<Picture> pictures)
        {
            if (pictures.Any(p => p.UserId == postMap.UserId && p.IsProfilePicture))
                postMap.User.ProfilPicture = pictures.Where(p => p.UserId == postMap.UserId && p.IsProfilePicture).FirstOrDefault().ImageName;
        }

    }
}
