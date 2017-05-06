using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facebook.Domain;
using Facebook.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.SystemFunctions;

namespace Facebook.WebApplication.Controllers
{
  [Route("api/[controller]")]
  //[Authorize(Policy = "Access Resources")]
  public class PostsController : Controller
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IDocumentDatabaseRepository<Post> _documentDatabaseRepository;

    public PostsController(IDocumentDatabaseRepository<Post> documentDatabaseRepository, UserManager<IdentityUser> userManager)
    {
      _documentDatabaseRepository = documentDatabaseRepository;
      _userManager = userManager;
    }


    // GET api/values
    [HttpGet]
    public async Task<IEnumerable<Post>> Get([FromQuery]string parentId, string comment)
    {
      if (!string.IsNullOrEmpty(parentId))
      {
        return await _documentDatabaseRepository.GetItemsWhereAsync(x => x.ParentId == new Guid(parentId));
      }
      return await _documentDatabaseRepository.GetItemsWhereAsync(x => x.ParentId.IsNull());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<Post> Get(string id)
    {
      return await _documentDatabaseRepository.GetItemAsync(id);
    }

    // POST api/values
    [HttpPost]
    public async Task<Document> Post([FromBody]Post value)
    {
      if (value.Id == default(Guid))
      {
        value.Id = Guid.NewGuid();
      }
      var identityId = User.FindFirst("sub").Value;
      value.UserId = new Guid(identityId);
      var identityUser = await _userManager.FindByIdAsync(identityId);
      var claims = await _userManager.GetClaimsAsync(identityUser);
      var firstName = claims.First(x => x.Type == "given_name");
      var lastName = claims.First(x => x.Type == "family_name");
      value.CreatedBy = $"{firstName.Value} {lastName.Value}";
      return await _documentDatabaseRepository.CreateItemAsync(value);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public async Task Put(string id, [FromBody]Post value)
    {
      await _documentDatabaseRepository.UpdateItemAsync(id, value);
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public async Task Delete(string id)
    {
      await _documentDatabaseRepository.DeleteItemAsync(id);
    }
  }
}
