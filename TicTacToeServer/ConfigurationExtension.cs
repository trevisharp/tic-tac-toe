using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TicTacToeServer;

public static class ConfigurationExtension
{
    public static string GetJwtSecret(this IConfiguration configuration)
    {
        var jwtSecret = configuration["JWTSecret"] 
            ?? throw new Exception("Missing JWT Secret.");
        return jwtSecret;
    }

    public static byte[] GetJwtSecretBytes(this IConfiguration configuration)
    {
        var jwtSecret = configuration.GetJwtSecret();
        var keyBytes = Encoding.UTF8.GetBytes(jwtSecret);
        return keyBytes;
    }

    public static SecurityKey GetJwtSecurityKey(this IConfiguration configuration)
    {
        var jwtBytes = configuration.GetJwtSecretBytes();
        return new SymmetricSecurityKey(jwtBytes);
    }
}