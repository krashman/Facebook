using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Facebook.Domain;
using Facebook.Repository;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.WebApplication.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository, SignInManager<User> signInManager, UserManager<User> userManager)
    {
      _userRepository = userRepository;
      _signInManager = signInManager;
      _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var model = new { Email = "", Password = "", RememberMe = false };
      var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
      if (result.Result.Succeeded)
      {
        return NotFound();
      }
      return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] User model)
    {
      if (!ModelState.IsValid) return BadRequest();

      var user = new User { UserName = model.Email, Email = model.Email };
      var result = await _userManager.CreateAsync(user, model.Password);
      if (!result.Succeeded) return new JsonResult(result);


      await _userManager.AddToRoleAsync(user, "user");
      var claims = new List<Claim> {
            new Claim(type: JwtClaimTypes.GivenName, value: model.FirstName),
            new Claim(type: JwtClaimTypes.FamilyName, value: model.LastName)
        };
      await _userManager.AddClaimsAsync(user, claims);
      return new JsonResult(result);

      // Assigns claims.
      //var claims = new List<Claim>
      //{
      //    new Claim(type: JwtClaimTypes.GivenName, value: "hello"),
      //    new Claim(type: JwtClaimTypes.FamilyName, value: "world"),
      //};

      //await _userManager.AddClaimsAsync(user, claims);
      // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
      // Send an email with this link
      //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
      //var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
      //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
      //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
      //await _signInManager.SignInAsync(user, isPersistent: false);
    }

  }
}