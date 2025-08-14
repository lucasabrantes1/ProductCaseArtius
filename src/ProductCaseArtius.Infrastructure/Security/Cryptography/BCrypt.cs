using BC = BCrypt.Net.BCrypt;
using ProductCaseArtius.Domain.Security.Cryptography;

namespace ProductCaseArtius.Infrastructure.Security.Cryptography;
internal class BCryptPasswordEncripter : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        return BC.HashPassword(password);
    }

    public bool Verify(string password, string passwordHash)
    {
        return BC.Verify(password, passwordHash);
    }
}
