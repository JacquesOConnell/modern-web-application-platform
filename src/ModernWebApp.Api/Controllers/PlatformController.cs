using Microsoft.AspNetCore.Mvc;

namespace ModernWebApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformController : ControllerBase
{
    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        return Ok(new
        {
            application = "Modern Web Application Platform",
            status = "Running",
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        });
    }
}
