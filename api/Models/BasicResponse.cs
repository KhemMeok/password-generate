using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITOAPP_API.Models
{
    public class BasicResponse
    {
        [JsonProperty(Order = 1)]
        public string status { get; set; }

        [JsonProperty(Order = 2)]
        public string message { get; set; }
    }
}
