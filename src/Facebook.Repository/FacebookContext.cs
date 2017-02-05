using Facebook.Domain;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Repository
{
    public class FacebookContext : DbContext
    {
        public FacebookContext()
        {
        }

        public FacebookContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=localhost;Database=Facebook;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }
    }
}