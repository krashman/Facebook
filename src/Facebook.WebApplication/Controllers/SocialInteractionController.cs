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
    private readonly IDocumentDatabaseRepository<SocialInteraction> _documentDatabaseRepository;

    public SocialInteractionController(IDocumentDatabaseRepository<SocialInteraction> documentDatabaseRepository)
    {
      _documentDatabaseRepository = documentDatabaseRepository;
    }


    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<SocialInteraction> Get(string postId)
    {
      return await _documentDatabaseRepository.GetItemAsync(postId);
    }

    // POST api/values
    [HttpPost]
    public async Task<Document> Post([FromBody]SocialInteraction value)
    {
      return await _documentDatabaseRepository.CreateItemAsync(value);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public async Task Put(string id, [FromBody]SocialInteraction value)
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
