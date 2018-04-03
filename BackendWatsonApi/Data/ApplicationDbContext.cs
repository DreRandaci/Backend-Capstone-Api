using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackendWatsonApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }

        public DbSet<Image> Image { get; set; }
        public DbSet<Place> Place { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<WatsonClassification> WatsonClassification { get; set; }
        public DbSet<UserPost> UserPost { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}