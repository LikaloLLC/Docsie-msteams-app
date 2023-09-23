using Newtonsoft.Json;

namespace Docsie.Application.Models
{
    public class WorkspaceMetaModel
    {
        public WorkspaceMetaModel()
        {
            this.WorkspaceModels = new List<WorkspaceModel>();
        }
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("results")]
        public List<WorkspaceModel> WorkspaceModels { get; set; }
    }
}
