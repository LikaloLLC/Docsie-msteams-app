using Docsie.Application.Models;
using Docsie.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DocsieTeamsApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthenticationController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet("token")]
        public async Task<IActionResult> IsAuthenticationTokenAvailableAysnc([FromQuery] string tenantId)
        {
            try
            {
                var authToken = await this.authService.GetAuthenticationTokenAsync(tenantId);
               
                if (string.IsNullOrEmpty(authToken))
                {
                    return Ok(new { status = HttpStatusCode.OK, isTokenAvailable = false });
                }

                return Ok(new { status = HttpStatusCode.OK, isTokenAvailable = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, errorMessage = ex.Message });
            }
        }

        [HttpPost("token")]
        public async Task<IActionResult> SaveAuthenticationTokenAysnc([FromBody] AuthTokenModel tokenModel)
        {
            try
            {
                if (string.IsNullOrEmpty(tokenModel.TenantId) || string.IsNullOrEmpty(tokenModel.Token)) 
                {
                    return Ok(new { status = HttpStatusCode.BadRequest, errorMessage = "tenantId or token can't be null" });
                }
                await this.authService.SaveAuthenticationTokenAsync(tokenModel);

                return Ok(new { status = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, errorMessage = ex.Message });
            }
        }
    }
}