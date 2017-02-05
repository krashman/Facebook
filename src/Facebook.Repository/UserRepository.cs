using Facebook.Domain;

namespace Facebook.Repository
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        public UserRepository(FacebookContext context) : base(context)
        {
        }
    }
}