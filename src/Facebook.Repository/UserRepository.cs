using Facebook.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Facebook.Repository
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        public UserRepository(FacebookDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}