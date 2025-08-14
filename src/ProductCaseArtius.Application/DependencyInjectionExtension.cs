using ProductCaseArtius.Application.AutoMapper;
using ProductCaseArtius.UseCases.Product;
using ProductCaseArtius.Application.UseCases.Login.DoLogin;
using ProductCaseArtius.Application.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace ProductCaseArtius.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        // Produtos - foco principal conforme requisitos da prova técnica
        services.AddScoped<ProductService>();
        
        // Login - necessário para autenticação
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        
        // Registro de usuário - necessário para cadastro
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}
