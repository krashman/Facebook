using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facebook.Domain;
using Facebook.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.SystemFunctions;
using User = Facebook.Domain.User;

namespace Facebook.WebApplication.Controllers
{
  [Route("api/[controller]")]
  //[Authorize(Policy = "Access Resources")]
  public class PostsController : Controller
  {
    private readonly UserManager<User> _userManager;
    private readonly ISocialInteractionsRepository _socialInteractionDocumentDatabaseRepository;
    private readonly IPostRepository _postRepository;

    public PostsController(IPostRepository postRepository, UserManager<User> userManager, ISocialInteractionsRepository socialInteractionDocumentDatabaseRepository)
    {
      _postRepository = postRepository;
      _userManager = userManager;
      _socialInteractionDocumentDatabaseRepository = socialInteractionDocumentDatabaseRepository;
    }


    // GET api/values
    [HttpGet]
    public async Task<IEnumerable<Post>> Get([FromQuery]string parentId, string comment)
    {
      if (!string.IsNullOrEmpty(parentId))
      {
        return await _postRepository.GetItemsWhereAsync(x => x.ParentId == new Guid(parentId));
      }
      return await _postRepository.GetItemsWhereAsync(x => x.ParentId.IsNull() || !x.ParentId.IsDefined());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<Post> Get(string id)
    {
      return await _postRepository.GetItemAsync(id);
    }

    // POST api/values
    [HttpPost]
    public async Task<Document> Post([FromBody]Post value)
    {
      var identityId = this.User.FindFirst("sub").Value;
      value.UserId = new Guid(identityId);
      var User = await _userManager.FindByIdAsync(identityId);
      var claims = await _userManager.GetClaimsAsync(User);
      var firstName = claims.First(x => x.Type == "given_name");
      var lastName = claims.First(x => x.Type == "family_name");
      value.CreatedBy = $"{firstName.Value} {lastName.Value}";
      
      return await _postRepository.CreateItemAsync(value);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public async Task Put(string id, [FromBody]Post value)
    {
      await _postRepository.UpdateItemAsync(id, value);
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public async Task Delete(string id)
    {
      await _postRepository.DeleteItemAsync(id);
    }
  }
}
