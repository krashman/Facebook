using Facebook.Domain;

namespace Facebook.Repository
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        public UserRepository(FacebookDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}