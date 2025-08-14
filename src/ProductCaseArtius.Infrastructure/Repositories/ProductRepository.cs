using Microsoft.EntityFrameworkCore;
using ProductCaseArtius.Domain.Entities;
using ProductCaseArtius.Domain.Repositories;
using ProductCaseArtius.Infrastructure.DataAccess;

namespace ProductCaseArtius.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductCaseArtiusDbContext _context;

        public ProductRepository(ProductCaseArtiusDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(long id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(long id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
