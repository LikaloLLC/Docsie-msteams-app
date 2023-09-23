using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Docsie.Application.Models
{
    public class DeploymentModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Name { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }
    }
}
