using Facebook.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _thingsRepository;

        public UserController(IUserRepository thingsRepository)
        {
            _thingsRepository = thingsRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_thingsRepository.GetAll());
        }
    }
}