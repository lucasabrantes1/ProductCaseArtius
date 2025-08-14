using ProductCaseArtius.Domain.Repositories.User;
using ProductCaseArtius.Infrastructure.DataAccess;

namespace ProductCaseArtius.Infrastructure.Repositories
{
    internal class UserWriteOnlyRepository : IUserWriteOnlyRepository
    {
        private readonly ProductCaseArtiusDbContext _context;

        public UserWriteOnlyRepository(ProductCaseArtiusDbContext context)
        {
            _context = context;
        }

        public async Task Add(ProductCaseArtius.Domain.Entities.User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ProductCaseArtius.Domain.Entities.User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
