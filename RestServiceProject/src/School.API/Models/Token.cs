using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace School.API.Models
{
    public class Token
    {
        [JsonProperty("token")]
        public string TokenString { get; set; }
    }
}



