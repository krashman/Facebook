using System;
using Newtonsoft.Json;

namespace Facebook.Domain
{
    public class Post : Entity
    {
        [JsonProperty(PropertyName = "id")]
        public override Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string DatePosted { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}