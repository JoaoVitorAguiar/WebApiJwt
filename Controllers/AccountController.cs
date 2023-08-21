using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiJwt.Data;
using WebApiJwt.Models;
using WebApiJwt.Services;
using WebApiJwt.ViewMoels.Account;

namespace WebApiJwt.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    // Registrar o usuário
    [HttpPost("v1/accounts/")]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] UserRegisterViewModel model,
        [FromServices] DataContext context
        )
    {
        var user = new User(model.Name,model.Email, model.Password);
        var role = context.Roles.FirstOrDefault(r => r.Name == "user");
        user.Roles.Add(role);
        await context.Users.AddAsync(user);

        await context.SaveChangesAsync();
        return Ok(model);
    }

    // Listar usuários
    [Authorize(Roles = "admin,manager")]
    [HttpGet("v1/accounts/")]
    public async Task<IActionResult> GetAsync(
        [FromServices] DataContext context
        )
    {
        var responses = new List<UserResponseViewModel>(); 
        var users = await context.Users.AsNoTracking().ToListAsync();
        foreach(var user in users)
        {
            responses.Add(new UserResponseViewModel
            {
                Name = user.Name,
                Email = user.Email,
            });
        }
        return Ok(responses);
    }

    // Login
    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> LoginAsync(
        [FromBody] UserLoginViewModel model,
        [FromServices] DataContext context,
        [FromServices] TokenService tokenService
        )
    {
        var user = await context.Users.Include(x => x.Roles).FirstOrDefaultAsync(
            x => x.Email == model.Email && x.PasswordHash == model.Password);
        if(user == null)
        {
            return BadRequest();
        }
        var token = tokenService.GenerateToken(user);
        return Ok(token);
    }

    // Criar gerente
    [Authorize(Roles = "admin")]
    [HttpGet("v1/accounts/create-manager/{id:int}")]
    public async Task<IActionResult> CreateManagerAsync(
        [FromRoute] int id, 
        [FromServices] DataContext context
        )
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if(user == null)
        {
            return BadRequest();
        }
        var role = await context.Roles.FirstOrDefaultAsync(x => x.Name == "manager");
        user.Roles.Add(role);
        var userResponse = new UserResponseViewModel
        {
            Name = user.Name,
            Email = user.Email
        };
        await context.SaveChangesAsync();
        return Ok(userResponse);
    }
}
