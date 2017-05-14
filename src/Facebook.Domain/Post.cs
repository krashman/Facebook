using System;
using Newtonsoft.Json;

namespace Facebook.Domain
{
    public class Post : Entity
    {
        [JsonProperty(PropertyName = "id")]
        public override Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; }
        public DateTime? DatePosted { get; set; } = DateTime.Now;

        public Guid? ParentId { get; set; }

        public Guid UserId { get; set; }
        public string CreatedBy { get; set; }


        public SocialInteractions SocialInteractions { get; set; }
    }
}