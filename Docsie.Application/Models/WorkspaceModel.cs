using Newtonsoft.Json;

namespace Docsie.Application.Models
{
    public class WorkspaceModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
