using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Facebook.Domain
{
    public class UserProfile
    {
        public string UserId { get; set; }

        public string Url { get; set; }
    }
}
