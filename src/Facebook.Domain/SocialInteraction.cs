using System;
using Newtonsoft.Json;

namespace Facebook.Domain
{
    public class SocialInteraction : Entity
    {
        [JsonProperty(PropertyName = "id")]
        public override Guid Id { get; set; } = Guid.NewGuid();

        public Guid PostId { get; set; }

        public int TotalComments { get; set; }

        public int TotalLikes { get; set; }
        
    }
}