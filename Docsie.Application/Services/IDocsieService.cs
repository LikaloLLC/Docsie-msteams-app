using Docsie.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docsie.Application.Services
{
    public interface IDocsieService
    {
        Task<WorkspaceMetaModel> ListWorkspacesAsync(string token);

        Task<DeploymentMetaModel> ListDeploymentsAsync(string token, string workspaceId);
    }
}
