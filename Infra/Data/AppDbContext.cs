using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.HasOne(o => o.User).WithMany().HasForeignKey("UserId"); 
                entity.HasMany(o => o.OrderItems)
                    .WithOne(b => b.Order)
                    .HasForeignKey("OrderId")
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.Property(e => e.Status)
                    .HasConversion(
                        v => v.ToString(), // Converter o enum C# para a string do ENUM no MySQL
                        v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v, true) // Converter a string do MySQL de volta para o enum C# (ignora o case)
                    );
            });
            
            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.Property(e => e.Type)
                    .HasConversion(
                        v => v.ToString(), // Converter o enum C# para a string do ENUM no MySQL
                        v => (ProductType)Enum.Parse(typeof(ProductType), v, true) // Converter a string do MySQL de volta para o enum C# (ignora o case)
                    );
            });

        }
        
    }
}