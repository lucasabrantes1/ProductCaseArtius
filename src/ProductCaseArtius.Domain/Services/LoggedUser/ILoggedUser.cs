using ProductCaseArtius.Domain.Entities;

namespace ProductCaseArtius.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> Get();
}
