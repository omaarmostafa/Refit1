﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refit1.Models
{
    public class UserLogin
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }

        [JsonProperty(".issued")]
        public string issued { get; set; }

        [JsonProperty(".expires")]
        public string expires { get; set; }
    }
}
