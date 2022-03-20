using Aniverse.Business.DTO_s.Page;
using Aniverse.Business.DTO_s.Picture;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.Interface;
using Aniverse.Core;
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
            var pageMap = _mapper.Map<List<PageGetDto>>(await _unitOfWork.PageRepository.GetAllPaginateAsync(page, size, p => p.CreationDate));
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
                    pageGet.ProfilePicture = pictures.Where(p => pageGet.Pagename == p.Page.Pagename && p.IsPageProfilePicture).FirstOrDefault().ImageName;
            }
            foreach (var pageItem in pageMap)
            {
                pageItem.FollowCount = pageFollow.Where(f => f.Page.Pagename == pageItem.Pagename).ToList().Count();
            }
            return pageMap;
        }
        public async Task<List<GetPictureDto>> GetPhotos(string pagename, HttpRequest request)
        {
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => p.Page.Pagename == pagename);
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

        public async Task<PageGetDto> GetPageAsync(string pagename, HttpRequest  request)
        {
            var pageMap = _mapper.Map<PageGetDto>(await _unitOfWork.PageRepository.GetAsync(p => p.Name == pagename));
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => p.Page.Pagename == pagename && (p.IsPageProfilePicture || p.IsPageCoverPicture));
            foreach (var picture in pictures)
            {
                if (picture.IsAnimalProfilePicture)
                {
                    pageMap.ProfilePicture = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
                }
                if (picture.IsAnimalCoverPicture)
                {
                    pageMap.CoverPicture = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");

                }
            }
            return pageMap;

        }

        public Task PageCreateAsync(PageCreateDto createDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
