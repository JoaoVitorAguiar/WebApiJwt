using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiJwt.Data;

namespace WebApiJwt.Controllers;

[ApiController]
[Authorize(Roles = "manager,admin")]
public class OrderManagerController : ControllerBase
{
    
    [HttpGet("v1/orders")]
    public async Task<IActionResult> GetAsync(
        [FromServices] DataContext context
        )
    {
        return Ok();
    }
}
