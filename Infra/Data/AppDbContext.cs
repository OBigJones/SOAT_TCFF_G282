using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infra.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DDbContext<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}