using ProductCaseArtius.Domain.Repositories;
using ProductCaseArtius.Domain.Repositories.User;
using ProductCaseArtius.Domain.Security.Cryptography;
using ProductCaseArtius.Domain.Security.Tokens;
using ProductCaseArtius.Infrastructure.DataAccess;
using ProductCaseArtius.Infrastructure.Repositories;
using ProductCaseArtius.Infrastructure.Security.Cryptography;
using ProductCaseArtius.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProductCaseArtius.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services);
        AddSecurity(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        // Repository principal para produtos
        services.AddScoped<IProductRepository, ProductRepository>();
        
        // Repositories para usuários (login e cadastro) - implementações separadas
        services.AddScoped<ProductCaseArtius.Domain.Repositories.User.IUserReadOnlyRepository, ProductCaseArtius.Infrastructure.Repositories.UserReadOnlyRepository>();
        services.AddScoped<ProductCaseArtius.Domain.Repositories.User.IUserWriteOnlyRepository, ProductCaseArtius.Infrastructure.Repositories.UserWriteOnlyRepository>();
    }

    private static void AddDbContext(IServiceCollection services)
    {
        // Entity Framework InMemory conforme requisitos da prova técnica
        services.AddDbContext<ProductCaseArtiusDbContext>(options =>
            options.UseInMemoryDatabase("ProductsInMemoryDatabase"));
    }
    
    private static void AddSecurity(IServiceCollection services, IConfiguration configuration)
    {
        // Serviços de segurança para autenticação
        services.AddScoped<IPasswordEncripter, BCryptPasswordEncripter>();
        services.AddScoped<IAccessTokenGenerator, JwtTokenGenerator>();
    }
}
