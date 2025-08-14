using Microsoft.EntityFrameworkCore;
using ProductCaseArtius.Domain.Repositories.User;
using ProductCaseArtius.Infrastructure.DataAccess;

namespace ProductCaseArtius.Infrastructure.Repositories
{
    internal class UserReadOnlyRepository : IUserReadOnlyRepository
    {
        private readonly ProductCaseArtiusDbContext _context;

        public UserReadOnlyRepository(ProductCaseArtiusDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email.Equals(email));
        }

        public async Task<ProductCaseArtius.Domain.Entities.User?> GetUserByEmail(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
        }
    }
}
