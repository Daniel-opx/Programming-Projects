using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Root
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
        
        
            [JsonProperty("id")]
            public int Id {  get; set; }

            [JsonProperty("title")]
            public string Title {  get; set; }
        

    }
}
