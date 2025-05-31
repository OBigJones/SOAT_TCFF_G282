using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.HasOne(o => o.User).WithMany().HasForeignKey("UserId"); 
                entity.HasMany(o => o.BurgerList)
                    .WithOne(b => b.Order)
                    .HasForeignKey("OrderId")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(o => o.Beverages)
                    .WithOne(b => b.Order)
                    .HasForeignKey("OrderId")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(o => o.Desserts)
                    .WithOne(d => d.Order)
                    .HasForeignKey("OrderId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BurgerEntity>().HasBaseType<ProductBase>();
            modelBuilder.Entity<BeverageEntity>().HasBaseType<ProductBase>();
            modelBuilder.Entity<DessertEntity>().HasBaseType<ProductBase>();
        }
        
    }
}