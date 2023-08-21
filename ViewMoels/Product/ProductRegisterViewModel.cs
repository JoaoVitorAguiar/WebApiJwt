using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiJwt.ViewMoels.Products;

public class ProductRegisterViewModel
{
    [Column(TypeName = "VARCHAR(80)")]
    public string Name { get; set; }
    [Required]
    public float Price { get; set; }
}
