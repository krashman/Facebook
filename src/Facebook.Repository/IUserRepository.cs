using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Facebook.Repository
{
    public interface IUserRepository : IRepository<IdentityUser>
    {
    }
}