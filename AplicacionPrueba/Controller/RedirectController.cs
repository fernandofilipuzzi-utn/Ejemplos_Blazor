using Microsoft.AspNetCore.Mvc;

namespace AplicacionPrueba.Controller;

[Route("api/[controller]")]
[ApiController]
public class RedirectController : ControllerBase
{
    [HttpGet]
    async public Task<IActionResult> GetRedirect(string url)
    {
        return Redirect(url);
    }
}
