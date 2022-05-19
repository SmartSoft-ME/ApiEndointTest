using ApiEndointTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEndointTest.Data
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public string DbPath { get; private set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "EndpointTest.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source={DbPath}");
    }
}
