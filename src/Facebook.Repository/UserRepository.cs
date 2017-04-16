using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Facebook.Repository
{
    public class UserRepository : Repository<IdentityUser> , IUserRepository
    {
        public UserRepository(FacebookDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}