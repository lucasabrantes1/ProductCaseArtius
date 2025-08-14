using ProductCaseArtius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductCaseArtius.Infrastructure.DataAccess;
public class ProductCaseArtiusDbContext : DbContext
{
    public ProductCaseArtiusDbContext(DbContextOptions options) : base(options) { }

    // Users para autenticação e Products para o caso de uso
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configuração da entidade User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Password).IsRequired();
            entity.Property(u => u.UserIdentifier).IsRequired();
        });
        
        // Configuração da entidade Product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            entity.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(50);
        });
    }
}
