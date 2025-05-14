using Microsoft.AspNetCore.Mvc;

namespace AplicacionPrueba.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RedirectController : ControllerBase
{
    [HttpGet]
    async public Task<IActionResult> Get(string url)
    {
        return Redirect(url);
    }
}
