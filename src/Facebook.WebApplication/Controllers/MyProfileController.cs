using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Facebook.Domain;
using Facebook.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Facebook.WebApplication.Controllers
{
  [Route("api/[controller]")]
  [Authorize(Policy = "Access Resources")]
  public class MyProfileController : Controller
  {
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IIdentityUserClaimRepository _identityUserClaimRepository;
    private readonly UserManager<User> _userManager;


    public MyProfileController(IUserProfileRepository userProfileRepository, IIdentityUserClaimRepository identityUserClaimRepository, UserManager<User> userManager)
    {
      _userManager = userManager;
      _userProfileRepository = userProfileRepository;
      _identityUserClaimRepository = identityUserClaimRepository;
    }



    // GET api/values/5
    [HttpGet]
    public async Task<UserProfile> Get()
    {
      var identityId = this.User.FindFirst("sub").Value;
      var profile = await _userProfileRepository.GetItemsWhereAsync(url => url.UserId == identityId);
      return profile.FirstOrDefault();
    }


    [HttpPut]
    public IActionResult Put([FromBody] IDictionary<string, string> model)
    {
      var identityId = this.User.FindFirst("sub").Value;
      foreach (var claim in model)
      {
        _identityUserClaimRepository.UpdateClaimGiven(identityId, claim.Key, claim.Value);
      }

      return Ok();
    }



    // POST api/values
    [HttpPost]
    public async Task Post(List<IFormFile> files)
    {

      // TODO: Update to multipart
      foreach (var formFile in files)
      {
        Console.WriteLine(formFile.FileName);
        Console.WriteLine(formFile.Length);
        using (var stream = new MemoryStream())
        {
          await formFile.CopyToAsync(stream);
          stream.Position = 0;
          var identityId = this.User.FindFirst("sub").Value;
          await _userProfileRepository.UploadFile(stream, formFile.FileName, formFile.ContentType, identityId);
        }
      }
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }

  public class Sample
  {
    public Dictionary<string, string> Claims { get; set; }
  }
}
