using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace TicTacToeServer.Services.Token;

public class JWTTokenService(IConfiguration configuration) : ITokenService
{
    public string Create(Guid id, string username)
    {
        var jwt = new JwtSecurityToken(
            claims: [
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, username)
            ],
            signingCredentials: new SigningCredentials(
                configuration.GetJwtSecurityKey(),
                SecurityAlgorithms.HmacSha256Signature
            )
        );
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(jwt);
    }
}