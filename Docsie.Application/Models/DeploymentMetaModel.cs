using Newtonsoft.Json;

namespace Docsie.Application.Models
{
    public class DeploymentMetaModel
    {
        public DeploymentMetaModel()
        {
            this.DeploymentModels = new List<DeploymentModel>();
        }
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("results")]
        public List<DeploymentModel> DeploymentModels { get; set; }
    }
}
