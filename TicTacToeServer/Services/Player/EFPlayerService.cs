using Microsoft.EntityFrameworkCore;

namespace TicTacToeServer.Services.Player;

using Models;
using Entities;
using Services.Token;

public class EFPlayerService(TicTacToeDbContext ctx, ITokenService tokenService) : IPlayerService
{
    public async Task<string?> Create(string username)
    {
        var query =
            from user in ctx.Users
            where user.Username == username
            select user;
        if (await query.AnyAsync())
            return null;
        
        var newUser = new ApplicationUser {
            Username = username
        };
        ctx.Add(newUser);
        await ctx.SaveChangesAsync();

        var token = tokenService.Create(newUser.Id, newUser.Username);
        return token;
    }

    public async Task<PlayerStats?> GetStats(Guid id)
    {
        var player = await ctx.FindAsync<ApplicationUser>(id);
        if (player is null)
            return null;

        var query = 
            from match in ctx.Matches
            where match.Player1Id == id || match.Player2Id == id
            where match.Status == MatchStatus.Finished
            select match;
        
        var matches = await query.ToListAsync();

        return new PlayerStats(
            player.Username,
            matches.Count,
            matches.Count(m => 
                (m.Winner == 1 && m.Player1Id == id) ||
                (m.Winner == 2 && m.Player2Id == id)
            )
        );
    }
}