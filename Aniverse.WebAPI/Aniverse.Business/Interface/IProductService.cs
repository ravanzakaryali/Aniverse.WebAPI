using Aniverse.Business.DTO_s.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aniverse.Business.Interface
{
    public interface IProductService
    {
        Task<List<ProductCategoryGetDto>> GetProductCategories();
        Task<ProductGetDto> ProductCreateAsync(ProductCreateDto productCreate, HttpRequest request);
        Task<List<ProductGetDto>> GetProductsAsync(int id,int page,int size ,HttpRequest request);
        Task<List<ProductGetDto>> GetAllAsync(int page, int size, HttpRequest request);
        Task<List<ProductGetDto>> GetUserSaveProducts(int page, int size, HttpRequest request);
        Task SaveProductAsync(int id);
        Task UnSaveProductAsync(int id);
    }
}
