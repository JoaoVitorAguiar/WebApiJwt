using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiJwt.Data;
using WebApiJwt.ViewMoels.OrderManager;

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
        var ordersResponses = new List<OrderResponseViewModel>();
        var orders = await context.Orders.Include(x=>x.User).Include(x=>x.Product).ToArrayAsync();
        foreach ( var order in orders) 
        {
            ordersResponses.Add(new OrderResponseViewModel
            {
                Id = order.Id,
                DateRequest = order.DateRequest,
                Product = order.Product.Name,
                User = order.User.Name
            });
        }
        return Ok(ordersResponses);
    }
}
