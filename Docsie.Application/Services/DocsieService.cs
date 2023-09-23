using Docsie.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Docsie.Application.Services
{
    public class DocsieService : IDocsieService
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        public DocsieService(IConfiguration configuration, HttpClient httpClient)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
        }

        public async Task<WorkspaceMetaModel> ListWorkspacesAsync(string token)
        {
            try
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var docsieBaseUrl = this.configuration.GetSection("Docsie:BaseUrl").Value;
                var fullUrl = $"{docsieBaseUrl}/workspaces/";
                var response = await this.httpClient.GetAsync(fullUrl);
                var contentString = await response.Content.ReadAsStringAsync();
                var workspaceMetaModel = JsonConvert.DeserializeObject<WorkspaceMetaModel>(contentString);

                return workspaceMetaModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        public async Task<DeploymentMetaModel> ListDeploymentsAsync(string token, string workspaceId)
        {
            try
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var docsieBaseUrl = this.configuration.GetSection("Docsie:BaseUrl").Value;
                var fullUrl = $"{docsieBaseUrl}/deployments/?workspace={workspaceId}";

                var response = await this.httpClient.GetAsync(fullUrl);
                var contentString = await response.Content.ReadAsStringAsync();
                var deploymentMetaModel = JsonConvert.DeserializeObject<DeploymentMetaModel>(contentString);

                return deploymentMetaModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
