using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Facebook.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.WebApplication.Controllers
{
  [Route("api/[controller]")]
  public class MyProfileController : Controller
  {
    private readonly IProfilePictureRepository _profilePictureRepository;


    public MyProfileController(IProfilePictureRepository profilePictureRepository)
    {
      _profilePictureRepository = profilePictureRepository;
    }



    // GET api/values/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
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
          await _profilePictureRepository.UploadFile(stream,
          formFile.FileName, formFile.ContentType);
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
}
