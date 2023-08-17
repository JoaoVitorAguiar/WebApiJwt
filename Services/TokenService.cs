using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiJwt.Extensions;
using WebApiJwt.Models;

namespace WebApiJwt.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        // Manipulador de Token
        var tokenHandler = new JwtSecurityTokenHandler();
        // O tokenHandler estepa uma array de bytes
        // Pegamos a chave que definimos no configuration
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var claims = user.GetClaims();

        // Configurações do token - Contém todas as informações
        // do token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        // Gerando o token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        // Gerando uma string baseada no token
        return tokenHandler.WriteToken(token);
    }
}
