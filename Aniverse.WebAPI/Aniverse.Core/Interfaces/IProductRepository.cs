using Aniverse.Core.Entites;
using System.Threading.Tasks;

namespace Aniverse.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> CreateProduct(Product product);
    }
}
