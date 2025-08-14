using ProductCaseArtius.Domain.Repositories;

namespace ProductCaseArtius.Infrastructure.DataAccess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly ProductCaseArtiusDbContext _dbContext;
    public UnitOfWork(ProductCaseArtiusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
