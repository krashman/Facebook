using System;
using System.Collections.Generic;

namespace Facebook.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Post> Posts { get; set; }
    }
}