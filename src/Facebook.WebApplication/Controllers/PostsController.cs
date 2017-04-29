using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Facebook.Domain;
using Facebook.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;

namespace Facebook.WebApplication.Controllers
{
  [Route("api/[controller]")]
  public class PostsController : Controller
  {
    private readonly IDocumentDatabaseRepository<Post> _documentDatabaseRepository;

    public PostsController(IDocumentDatabaseRepository<Post> documentDatabaseRepository)
    {
      _documentDatabaseRepository = documentDatabaseRepository;
    }


    // GET api/values
    [HttpGet]
    public async Task<IEnumerable<Post>> Get()
    {
        return await _documentDatabaseRepository.GetAllItemsAsync();
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
