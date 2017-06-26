using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Facebook.Domain
{
    public class ProfilePictureUrl
    {
        public string UserId { get; set; }

        public string Url { get; set; }
    }
}
