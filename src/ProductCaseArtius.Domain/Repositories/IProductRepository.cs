using ProductCaseArtius.Domain.Entities;

namespace ProductCaseArtius.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(long id);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(long id);
    }
}
