using Aniverse.Business.DTO_s.Comment;
using Aniverse.Business.DTO_s.Page;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.DTO_s.User;
using Aniverse.Business.Exceptions;
using Aniverse.Business.Exceptions.FileExceptions;
using Aniverse.Business.Extensions;
using Aniverse.Business.Helpers;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using Aniverse.Core.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aniverse.Business.Implementations
{
    public class PageService : IPageService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IHostEnvironment _hostEnvironment;


        public PageService(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<PageGetDto>> GetAllAsync(int page, int size, HttpRequest request)
        {
            var pageMap = _mapper.Map<List<PageGetDto>>(await _unitOfWork.PageRepository.GetAllPaginateAsync(page, size, p => p.CreationDate, null, "PageFollow"));
            var pageNames = pageMap.Select(p => p.Pagename);
            var pageFollow = await _unitOfWork.PageFollowRepository.GetAllAsync(p => pageNames.Contains(p.Page.Pagename));
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => p.IsPageProfilePicture);

            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            foreach (var pageGet in pageMap)
            {
                if (pictures.Any(p => pageGet.Pagename == p.Page.Pagename && p.IsPageProfilePicture))
                    pageGet.ProfilPicture = pictures.Where(p => pageGet.Pagename == p.Page.Pagename && p.IsPageProfilePicture).FirstOrDefault().ImageName;
            }
            return pageMap;
        }
        public async Task<List<GetPictureDto>> GetPhotos(string pagename, int page, int size, HttpRequest request)
        {
            var pictures = await _unitOfWork.PictureRepository.GetAllPaginateAsync(page, size, p => p.Id, p => p.Page.Pagename == pagename && p.ProductId == null);
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            return _mapper.Map<List<GetPictureDto>>(pictures);
        }

        public Task<List<PageGetDto>> GetFollowPages(int page, int size)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PageGetDto> GetPageAsync(string pagename, HttpRequest request)
        {
            var pageMap = _mapper.Map<PageGetDto>(await _unitOfWork.PageRepository.GetAsync(p => p.Name == pagename, "PageFollow"));
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => p.Page.Pagename == pagename && (p.IsPageProfilePicture || p.IsPageCoverPicture));
            foreach (var picture in pictures)
            {
                if (picture.IsPageProfilePicture)
                {
                    pageMap.ProfilPicture = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
                }
                if (picture.IsPageCoverPicture)
                {
                    pageMap.CoverPicture = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");

                }
            }
            return pageMap;

        }

        public async Task PageCreateAsync(PageCreateDto createDto)
        {
            var UserLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var pagename = await _unitOfWork.PageRepository.GetAsync(p => p.Pagename == createDto.Pagename);
            if (pagename != null)
            {
                throw new AlreadyException("Already animalname");
            }
            var newPage = _mapper.Map<Page>(createDto);
            newPage.UserId = UserLoginId;
            await _unitOfWork.PageRepository.CreateAsync(newPage);
            await _unitOfWork.SaveAsync();
        }
        public async Task<List<PostPageGetDto>> GetPostsAsync(int page, int size, string pagename, HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var posts = await _unitOfWork.PostRepository.GetAllPaginateAsync(page, size, p => p.CreationDate, p => p.Page.Pagename == pagename, "User", "Likes", "Page", "Comments", "Comments.User");
            var postIds = posts.Select(f => f.Id);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => posts.Contains(p.Post) || p.Page.Pagename == pagename || p.UserId == userLoginId);
            PictureDbName(pictures, request);
            var postMap = _mapper.Map<List<PostPageGetDto>>(posts);
            var comments = _mapper.Map<List<CommentGetDto>>(await _unitOfWork.CommentRepository.GetAllAsync(c => postIds.Contains(c.PostId), "User"));
            CommentUserProfilePicture(pictures, comments);
            PostPageProfile(postMap, pictures);
            return postMap;

        }

        public async Task ProfileCreate(int id, ProfileCreateDto profileCreate)
        {
            if (!profileCreate.ImageFile.CheckFileSize(10000))
                throw new FileTypeException("File max size 10 mb");
            if (!profileCreate.ImageFile.CheckFileType("image/"))
                throw new FileSizeException("File type must be image");
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var picture = new Picture
            {
                IsPageProfilePicture = true,
                PageId = id,
                ImageName = await profileCreate.ImageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                UserId = userLoginId,
            };
            var profilePictureDb = await _unitOfWork.PictureRepository.GetAsync(p => p.UserId == userLoginId && p.PageId == id && p.IsPageProfilePicture == true);
            await _unitOfWork.PictureRepository.CreateAsync(picture);
            if (profilePictureDb != null)
            {
                profilePictureDb.IsPageProfilePicture = false;
            }
            await _unitOfWork.SaveAsync();
        }
        public async Task CoverCreate(int id, ProfileCreateDto profileCreate)
        {
            if (!profileCreate.ImageFile.CheckFileSize(10000))
                throw new FileTypeException("File max size 10 mb");
            if (!profileCreate.ImageFile.CheckFileType("image/"))
                throw new FileSizeException("File type must be image");
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var picture = new Picture
            {
                IsPageCoverPicture = true,
                PageId = id,
                ImageName = await profileCreate.ImageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                UserId = userLoginId,
            };
            var profilePictureDb = await _unitOfWork.PictureRepository.GetAsync(p => p.UserId == userLoginId && p.PageId == id && p.IsPageCoverPicture == true);
            await _unitOfWork.PictureRepository.CreateAsync(picture);
            if (profilePictureDb != null)
            {
                profilePictureDb.IsPageProfilePicture = false;
            }
            await _unitOfWork.SaveAsync();
        }
        private void PictureDbName(List<Picture> pictures, HttpRequest request)
        {
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
        }
        private void CommentUserProfilePicture(List<Picture> pictures, List<CommentGetDto> comments)
        {
            foreach (var comment in comments)
            {
                if (pictures.Any(p => p.UserId == comment.UserId && p.IsProfilePicture))
                    comment.User.ProfilPicture = pictures.Where(p => p.UserId == comment.UserId && p.IsProfilePicture).FirstOrDefault().ImageName;
            }
        }
        public async Task<List<UserGetDto>> GetPageFollowersUser(int id, int page, int size, HttpRequest request)
        {
            var pageFollow = await _unitOfWork.PageFollowRepository.GetAllPaginateAsync(page, size, p => p.FollowDate, p => p.PageId == id);
            var userIds = pageFollow.Select(p => p.UserId).ToList();
            var users = _mapper.Map<List<UserGetDto>>(await _unitOfWork.UserRepository.GetAllAsync(u => userIds.Contains(u.Id)));
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => userIds.Contains(p.UserId));
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            foreach (var user in users)
            {
                if (pictures.Any(p => p.UserId == user.Id && p.IsProfilePicture))
                    user.ProfilPicture = pictures.Where(p => p.UserId == user.Id && p.IsProfilePicture).FirstOrDefault().ImageName;
            }
            return users;
        }
        public async Task PageFollow(int id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            PageFollow pageFollow = new PageFollow()
            {

                PageId = id,
                UserId = userLoginId,
            };
            await _unitOfWork.PageFollowRepository.CreateAsync(pageFollow);
            await _unitOfWork.SaveAsync();
        }
        public async Task PageUnfollow(int id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            PageFollow pageFollow = await _unitOfWork.PageFollowRepository.GetAsync(p=>p.UserId == userLoginId && p.PageId == id);
            _unitOfWork.PageFollowRepository.Delete(pageFollow);
            await _unitOfWork.SaveAsync();
        }
        public async Task<List<PageGetDto>> GetUserFollowPages(string id, HttpRequest request)
        {
            var pageFollow = await _unitOfWork.PageFollowRepository.GetAllAsync(p => p.UserId == id);
            var pageIds = pageFollow.Select(p => p.PageId).ToList();
            var pages = _mapper.Map<List<PageGetDto>>(await _unitOfWork.PageRepository.GetAllAsync(p => pageIds.Contains(p.Id)));
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => pageIds.Contains((int)p.PageId) && p.IsPageProfilePicture);
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
            foreach (var pageGet in pages)
            {
                if (pictures.Any(p => pageGet.Id == p.PageId && p.IsPageProfilePicture))
                    pageGet.ProfilPicture = pictures.Where(p => pageGet.Id == p.PageId && p.IsPageProfilePicture).FirstOrDefault().ImageName;
            }
            return pages;
        }
        public async Task PageUpdate(int id,PageUpdateDto pageUpdate)
        {
            var page = await _unitOfWork.PageRepository.GetAsync(p=>p.Id == id);
            if(page is null)
                throw new NotFoundException("Page is not found");
            page.About = pageUpdate.About;
            page.Website = pageUpdate.Website;
            await _unitOfWork.SaveAsync();
        }
        private void PostPageProfile(List<PostPageGetDto> postMap, List<Picture> pictures)
        {
            foreach (var post in postMap)
            {
                if (pictures.Any(p => p.PageId == post.Page.Id && p.IsPageProfilePicture))
                    post.Page.ProfilPicture = pictures.Where(p => p.PageId == post.Page.Id && p.IsPageProfilePicture).FirstOrDefault().ImageName;
            }
        }


    }
}
