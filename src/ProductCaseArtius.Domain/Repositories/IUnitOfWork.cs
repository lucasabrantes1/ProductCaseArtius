namespace ProductCaseArtius.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
