using System;
using System.Threading.Tasks;
using Facebook.Domain;
using Facebook.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;

namespace Facebook.WebApplication.Controllers
{
  [Route("api/[controller]")]
  //[Authorize(Policy = "Access Resources")]
  public class SocialInteractionController : Controller
  {
    private readonly ISocialInteractionsRepository _socialInteractionsRepository;

    public SocialInteractionController(ISocialInteractionsRepository socialInteractionsRepository)
    {
      _socialInteractionsRepository = socialInteractionsRepository;
    }


    // GET api/values/5
    [HttpGet]
    public async Task<SocialInteractions> Get([FromQuery] Guid postId)
    {
      return await _socialInteractionsRepository.GetItemByPostId(postId);
    }

    // POST api/values
    [HttpPost]
    public async Task<Document> Post([FromBody]SocialInteractions value)
    {
      return await _socialInteractionsRepository.CreateItemAsync(value);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public async Task Put(string id, [FromBody]SocialInteractions value)
    {
      await _socialInteractionsRepository.UpdateItemAsync(id, value);
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public async Task Delete(string id)
    {
      await _socialInteractionsRepository.DeleteItemAsync(id);
    }
  }
}
