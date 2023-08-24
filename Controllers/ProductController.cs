using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiJwt.Data;
using WebApiJwt.Models;
using WebApiJwt.ViewMoels.Products;

namespace WebApiJwt.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    // Listar produtos
    [HttpGet("v1/products")]
    public async Task<IActionResult> GetAsync(
        [FromServices] DataContext context
        )
    {
        var products = await context.Products.ToListAsync();
        return Ok(products);
    }

    // Registrar produto
    [Authorize(Roles = "admin,manager")]
    [HttpPost("v1/products")]
    public async Task<IActionResult> PostAsync(
        [FromBody] ProductRegisterViewModel model,
        [FromServices] DataContext context
        )
    {
        var product = new Product(model.Name, model.Price);
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return Ok(product);
    }
}
