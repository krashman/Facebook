using System.Collections.Generic;

namespace Facebook.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public string Email { get; set; }
        
        public string Password { get; set; }

        public List<Post> Posts { get; set; }
    }
}