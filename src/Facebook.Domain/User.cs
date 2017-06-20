using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Facebook.Domain
{
    public class User : IdentityUser
    {
        public byte[] UserPhoto { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string Password { get; set; }
    }
}