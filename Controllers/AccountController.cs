using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Data;
using WebApiJwt.Models;
using WebApiJwt.ViewMoels.Account;

namespace WebApiJwt.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("v1/accounts/")]
    public async Task<IActionResult> PostAsync(
        [FromBody] RegisterViewModel model,
        [FromServices] DataContext context
        )
    {
        var user = new User(model.Name,model.Email, model.Password);

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return Ok(user);
    }
}
