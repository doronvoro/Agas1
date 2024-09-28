using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SecureDataController : ControllerBase
{
    [HttpGet("secret")]
    public IActionResult GetSecretData()
    {
        return Ok("This is protected data.");
    }
}
