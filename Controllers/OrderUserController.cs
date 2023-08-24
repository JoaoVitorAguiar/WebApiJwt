using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiJwt.Data;
using WebApiJwt.Models;
using WebApiJwt.ViewMoels.OrderUser;

namespace WebApiJwt.Controllers;

[Authorize]
[ApiController]
public class OrderUserController : ControllerBase
{
    [HttpGet("v1/user/orders")]
    public async Task<IActionResult> GetAsync(
        [FromServices] DataContext context
        )
    {
        var email = User.Identity.Name;
        var orderResponse = new List<OrderUserResponseViewModel>();

        var userOrders = await context.Orders
            .Include(x=>x.Product)
            .Where(x=>x.User.Email.Contains(email))
            .ToListAsync();
        foreach( var order in userOrders )
        {
            orderResponse.Add(new OrderUserResponseViewModel
            {
                ProductName = order.Product.Name,
                DateRequest = order.DateRequest
            });
        }
        return Ok(orderResponse);
    }

    [HttpPost("v1/user/orders")]
    public async Task<IActionResult> PostAsync(
        [FromServices] DataContext context,
        [FromBody] OrderUserRegisterViewModel model
        )
    {
        var product = await context.Products.FirstOrDefaultAsync(x=>x.Name == model.ProductName);
        if ( product == null )
        {
            BadRequest($"Esse produto({model.ProductName}) não existe");
        }
        var email = User.Identity.Name;
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if ( user == null )
        {
            BadRequest("Usuário não encontrado");
        }
        var order = new Order
        {
            User = user,
            Product = product,
            DateRequest = model.DateRequest
        };
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
        return Ok(order);
    }
}
