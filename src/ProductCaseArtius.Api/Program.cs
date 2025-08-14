using ProductCaseArtius.Api.Filters;
using ProductCaseArtius.Application;
using ProductCaseArtius.Infrastructure;
using ProductCaseArtius.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração básica da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


// Configuração do Swagger com autenticação JWT
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Products API",
        Version = "v1",
        Description = "API REST para cadastro de produtos e usuários"
    });

    // Configuração de autenticação JWT no Swagger
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu_token_aqui}"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


//builder.Services.AddDbContext<ProductCaseArtiusDbContext>(options =>
//    options.UseInMemoryDatabase("ProductsInMemoryDb"));

builder.Services.AddDbContext<ProductCaseArtiusDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Connection");
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        b => b.MigrationsAssembly("ProductCaseArtius.Infrastructure")
    );
});




// Configuração de autenticação JWT
var jwtSigningKey = builder.Configuration.GetSection("Settings:Jwt:SigningKey").Value;
var key = Encoding.UTF8.GetBytes(jwtSigningKey!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Registro das dependências
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Filtro para tratamento de exceções
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

var app = builder.Build();





// Pipeline de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API V1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAll"); // <-- Aqui no pipeline

// Middleware de autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed de dados inicial para facilitar testes
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductCaseArtiusDbContext>();
    
    // Verifica se já existe usuário, se não cria um usuário padrão
    if (!context.Users.Any())
    {
        var passwordEncripter = scope.ServiceProvider.GetRequiredService<ProductCaseArtius.Domain.Security.Cryptography.IPasswordEncripter>();
        
        context.Users.Add(new ProductCaseArtius.Domain.Entities.User
        {
            Id = 1,
            Name = "Admin User",
            Email = "admin@test.com",
            Password = passwordEncripter.Encrypt("123456"),
            UserIdentifier = Guid.NewGuid(),
            Role = "ADMIN"
        });
        
        context.SaveChanges();
    }
}

app.Run();

public partial class Program { }