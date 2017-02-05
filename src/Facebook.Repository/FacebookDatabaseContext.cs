using Facebook.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Repository
{
    public class FacebookDatabaseContext : IdentityDbContext<IdentityUser>
    {
        public FacebookDatabaseContext()
        {
        }

        public FacebookDatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Maybeh { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=localhost;Database=Facebook;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }
    }
}