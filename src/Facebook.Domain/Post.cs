using System;

namespace Facebook.Domain
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string DatePosted { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}