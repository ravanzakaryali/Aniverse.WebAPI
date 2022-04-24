using Aniverse.Business.DTO_s.Product;
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
    public class ProductService : IProductService
    {

        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly IHostEnvironment _hostEnvironment;
        public readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper,IHostEnvironment hostEnvironment ,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ProductCategoryGetDto>> GetProductCategories()
        {
            return _mapper.Map<List<ProductCategoryGetDto>>(await _unitOfWork.ProductCategoryRepository.GetAllAsync());
        }
        public async Task<ProductGetDto> ProductCreateAsync(ProductCreateDto productCreate, HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            productCreate.UserId = userLoginId;
            if (productCreate.ImageFile != null)
            {
                foreach (var file in productCreate.ImageFile)
                {
                    if (!file.CheckFileSize(10000))
                        throw new FileTypeException("File max size 100 mb");
                    if (!file.CheckFileType("image/"))
                        throw new FileSizeException("File type must be image");
                }
                productCreate.Pictures = new List<ProductImageDto>();
                foreach (var picture in productCreate.ImageFile)
                {
                    var image = new ProductImageDto
                    {
                        UserId = userLoginId,
                        PageId = productCreate.PageId,
                        ImageName = await picture.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images"),
                    };
                    productCreate.Pictures.Add(image);
                }
            }
            var newProduct = await _unitOfWork.ProductRepository.CreateProduct(_mapper.Map<Product>(productCreate));
            await _unitOfWork.SaveAsync();
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => newProduct.Id == p.ProductId);
            PictureDbName(pictures, request);
            return _mapper.Map<ProductGetDto>(newProduct);
        }
        public async Task<List<ProductGetDto>> GetProductsAsync(int id, int page, int size, HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var products = await _unitOfWork.ProductRepository.GetAllPaginateAsync(page, size, p => p.CreationDate, p => p.PageId == id,"Pictures");
            var productsId = products.Select(p => p.Id);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => p.PageId == id && productsId.Contains((int)p.ProductId));
            PictureDbName(pictures, request);
            var productsMap = _mapper.Map<List<ProductGetDto>>(products);
            var productSave = await _unitOfWork.SaveProductRepository.GetAllAsync(s=>s.UserId == userLoginId);
            var productSaveIds = productSave.Select(p => p.ProductId);
            ProductSaveIds(productsMap, productSaveIds);
            return productsMap;
        } 
        public async Task<List<ProductGetDto>> GetAllAsync(int page, int size, HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var products = await _unitOfWork.ProductRepository.GetAllPaginateAsync(page, size, p => p.CreationDate, null, "Pictures");
            var productsId = products.Select(p => p.Id);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => productsId.Contains((int)p.ProductId));
            PictureDbName(pictures, request);
            var productsMap = _mapper.Map<List<ProductGetDto>>(products);
            var productSave = await _unitOfWork.SaveProductRepository.GetAllAsync(s => s.UserId == userLoginId);
            var productSaveIds = productSave.Select(p => p.ProductId);
            ProductSaveIds(productsMap, productSaveIds);
            return productsMap;
        }
        public async Task SaveProductAsync(int id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var product = await _unitOfWork.ProductRepository.GetAsync(p=>p.Id == id);
            if (product is null)
                throw new NotFoundException("Product is bot found");
            SaveProduct save = new SaveProduct
            {
                ProductId = product.Id,
                UserId = userLoginId,
            };
            await _unitOfWork.SaveProductRepository.CreateAsync(save);
            await _unitOfWork.SaveAsync();
        }
        public async Task UnSaveProductAsync(int id)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var saveProduct = await _unitOfWork.SaveProductRepository.GetAsync(s=>s.ProductId == id && s.UserId == userLoginId);
            if (saveProduct is null)
                throw new NotFoundException("Product is not found");
            _unitOfWork.SaveProductRepository.Delete(saveProduct);
            await _unitOfWork.SaveAsync();
        }
        public async Task<List<ProductGetDto>> GetUserSaveProducts(int page, int size, HttpRequest request)
        {
            var userLoginId = _httpContextAccessor.HttpContext.User.GetUserId();
            var saveProduct = await _unitOfWork.SaveProductRepository.GetAllPaginateAsync(page,size,s=>s.SaveAddDate,s => s.UserId == userLoginId, "Product", "Product.Pictures");
            var products = saveProduct.Select(p => p.Product);
            var productsId = saveProduct.Select(p => p.ProductId);
            var pictures = await _unitOfWork.PictureRepository.GetAllAsync(p => productsId.Contains((int)p.ProductId));
            PictureDbName(pictures, request);
            var productsMap = _mapper.Map<List<ProductGetDto>>(products);
            var productSave = await _unitOfWork.SaveProductRepository.GetAllAsync(s => s.UserId == userLoginId);
            var productSaveIds = productSave.Select(p => p.ProductId);
            ProductSaveIds(productsMap, productSaveIds);
            return productsMap;
        }
        private void PictureDbName(List<Picture> pictures, HttpRequest request)
        {
            foreach (var picture in pictures)
            {
                picture.ImageName = String.Format($"{request.Scheme}://{request.Host}{request.PathBase}/Images/{picture.ImageName}");
            }
        }
        private void ProductSaveIds(List<ProductGetDto> productsMap, IEnumerable<int> productSaveIds)
        {
            foreach (var product in productsMap)
            {
                if (productSaveIds.Contains(product.Id))
                {
                    product.IsSave = true;
                }
            }
        } 
    }
}
