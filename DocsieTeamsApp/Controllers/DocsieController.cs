using Docsie.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DocsieTeamsApp.Controllers
{
    [ApiController]
    [Route("api/docsie")]
    public class DocsieController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IDocsieService docsieService;
        public DocsieController(IAuthService authService, IDocsieService docsieService)
        {
            this.authService = authService;
            this.docsieService = docsieService;
        }

        [HttpGet("workspaces")]
        public async Task<IActionResult> GetWorkspacesAsync([FromQuery] string tenantId)
        {
            try
            {
                var token = await this.authService.GetAuthenticationTokenAsync(tenantId);

                if(string.IsNullOrEmpty(token))
                {
                    return Ok(new { status = HttpStatusCode.Unauthorized, errorMessage = "API Key not found" });
                }

                var workspaceMetaModel = await this.docsieService.ListWorkspacesAsync(token);

                var response = new { status = HttpStatusCode.OK, workspaceMetaModel };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, errorMessage = ex.Message });
            }
        }

        [HttpGet("deployments")]
        public async Task<IActionResult> GetDeploymentsAsync([FromQuery] string tenantId, [FromQuery] string workspaceId)
        {
            try
            {
                var token = await this.authService.GetAuthenticationTokenAsync(tenantId);

                if (string.IsNullOrEmpty(token))
                {
                    return Ok(new { status = HttpStatusCode.Unauthorized, errorMessage = "API Key not found" });
                }

                if (string.IsNullOrEmpty(workspaceId))
                {
                    return Ok(new { status = HttpStatusCode.BadRequest, errorMessage = "WorkspaceId not found" });
                }

                var deploymentMetaModel = await this.docsieService.ListDeploymentsAsync(token, workspaceId);

                var response = new { status = HttpStatusCode.OK, deploymentMetaModel };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, errorMessage = ex.Message });
            }
        }
    }
}