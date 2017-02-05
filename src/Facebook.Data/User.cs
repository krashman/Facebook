using System;
using System.Collections.Generic;

namespace Facebook.Data
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Post> Posts { get; set; }
    }
}