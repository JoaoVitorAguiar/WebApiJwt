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
        try
        {
            if(context.Users.FirstOrDefault(x => x.Email == model.Email) == null)
            {
                return StatusCode(400, "Este e-mail já foi cadastrado");
            }
            var user = new User(model.Name, model.Email, model.Password);
            var role = context.Roles.FirstOrDefault(r => r.Name == "user");
            user.Roles.Add(role);

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            // Response
            var response = new UserResponseViewModel
            {
                Name = user.Name,
                Email = user.Email
            };
            return Ok(response);
        }
        catch (Exception)
        {
            return StatusCode(500, "Falha interna no servido");
        }
        
    }

    // Listar usuários
    [Authorize(Roles = "admin,manager")]
    [HttpGet("v1/accounts/")]
    public async Task<IActionResult> GetAsync(
        [FromServices] DataContext context
        )
    {
        var responses = new List<UserResponseViewModel>();
        try 
        {
            var users = await context.Users.AsNoTracking().ToListAsync();
            foreach (var user in users)
            {
                responses.Add(new UserResponseViewModel
                {
                    Name = user.Name,
                    Email = user.Email,
                });
            }
            return Ok(responses);
        }
        catch (Exception)
        {
            return StatusCode(500, "Falha interna no servido");
        }
    }

    // Login
    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> LoginAsync(
        [FromBody] UserLoginViewModel model,
        [FromServices] DataContext context,
        [FromServices] TokenService tokenService
        )
    {
        try
        {
            var user = await context.Users.Include(x => x.Roles).FirstOrDefaultAsync(
            x => x.Email == model.Email && x.PasswordHash == model.Password);
            if (user == null)
            {
                return StatusCode(401, "Usuário ou senha inválidos");
            }
            var token = tokenService.GenerateToken(user);
            return Ok(token);
        }
        catch(Exception)
        {
            return StatusCode(500, "Falha interna no servido");
        }
    }

    // Criar gerente
    [Authorize(Roles = "admin")]
    [HttpGet("v1/accounts/create-manager/{id:int}")]
    public async Task<IActionResult> CreateManagerAsync(
        [FromRoute] int id, 
        [FromServices] DataContext context
        )
    {
        try 
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return StatusCode(401, "Usuário não encontrado");
            }
            var role = await context.Roles.FirstOrDefaultAsync(x => x.Name == "manager");
            if (role == null)
            {
                return StatusCode(500, "Perfil não encontrado");
            }
            user.Roles.Add(role);
            var userResponse = new UserResponseViewModel
            {
                Name = user.Name,
                Email = user.Email
            };
            await context.SaveChangesAsync();
            return Ok(userResponse);
        }
        catch( Exception )
        {
            return StatusCode(500, "Falha interna no servido");
        }
    }
}
