using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BackendWatsonApi.Models;

namespace BackendWatsonApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }

        public DbSet<UserPost> Image { get; set; }
        public DbSet<Place> Place { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<WatsonClassification> WatsonClassification { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}