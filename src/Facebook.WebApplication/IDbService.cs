using System.Threading.Tasks;

namespace Facebook.WebApplication
{
    public interface IDbService
    {
        Task populate();
    }
}