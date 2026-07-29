using Microsoft.EntityFrameworkCore;
using MyFristProject.Entity;

namespace MyFristProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Prodect> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}