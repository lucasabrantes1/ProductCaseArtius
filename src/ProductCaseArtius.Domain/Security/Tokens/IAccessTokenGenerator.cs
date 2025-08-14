using ProductCaseArtius.Domain.Entities;

namespace ProductCaseArtius.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    string Generate(User user);
}
