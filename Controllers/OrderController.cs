using Microsoft.AspNetCore.Mvc;

namespace WebApiJwt.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    [HttpGet("v1/orders")]
    public async Task<IActionResult> GetAsync()
    {
        
    }
}
