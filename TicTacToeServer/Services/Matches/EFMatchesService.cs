using Microsoft.EntityFrameworkCore;

namespace TicTacToeServer.Services.Matches;

using Models;
using Entities;

public class EFMatchesService(TicTacToeDbContext ctx) : IMatchesService
{
    public async Task<List<Match>> Find(Guid playerId, string status, int page, int limit)
    {
        var statusCode = status switch
        {
            "pairing" => MatchStatus.Pairing,
            "in game" => MatchStatus.InGame,
            "finished" => MatchStatus.Finished,
            "all" => MatchStatus.All,
            _ => MatchStatus.Unknown
        };

        var query = 
            from match in ctx.Matches
            where match.Player1Id == playerId || match.Player2Id == playerId
            where (match.Status & statusCode) > 0
            select match;
        
        return await query
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<Match?> FindOne(Guid matchId)
        => await ctx.FindAsync<Match>(matchId);

    public async Task<Match?> FindPair(Guid playerId)
    {
        var query = 
            from match in ctx.Matches
            where match.Player1Id != playerId
            where match.Status == MatchStatus.Pairing
            select match;
        var otherMatch = await query.FirstOrDefaultAsync();
        if (otherMatch is null)
            return await CreateNewMatch(playerId);
        
        otherMatch.Player2Id = playerId;
        otherMatch.Status = MatchStatus.InGame;
        otherMatch.ActivePlayer = Random.Shared.Next(2) + 1;
        otherMatch.PlayTime = DateTime.UtcNow.AddHours(1);
        await ctx.SaveChangesAsync();
        return otherMatch;
    }

    async Task<Match> CreateNewMatch(Guid playerId)
    {
        var match = new Match {
            Player1Id = playerId,
            Status = MatchStatus.Pairing
        };
        ctx.Add(match);
        await ctx.SaveChangesAsync();

        return match;
    }

    public async Task<PlayResult?> Play(Guid matchId, int playCode)
    {
        var match = await FindOne(matchId);
        if (match is null)
            return new PlayResult(false, "The match do not exists.");
        
        int positionMask = 0b11;
        int play = match.ActivePlayer;
        for (int i = playCode; i > 0; i--,
            positionMask <<= 2, play <<= 2
        );

        if ((match.BoardData & positionMask) > 0)
            return new PlayResult(false, "The position already contains data.");
        
        match.BoardData += play;
        match.PlayTime = DateTime.UtcNow.AddHours(1);
        match.ActivePlayer = match.ActivePlayer == 1 ? 2 : 1;

        CheckVictory(match);

        await ctx.SaveChangesAsync();
        return new PlayResult(true, "Move made successfully");
    }

    static void CheckVictory(Match match)
    {
        int mask1 = 0b01_01_01_00_00_00_00_00_00;
        int mask2 = 0b00_00_00_01_01_01_00_00_00;
        int mask3 = 0b00_00_00_00_00_00_01_01_01;
        int mask4 = 0b01_00_00_01_00_00_01_00_00;
        int mask5 = 0b00_01_00_00_01_00_00_01_00;
        int mask6 = 0b00_00_01_00_00_01_00_00_01;
        int mask7 = 0b00_00_01_00_01_00_01_00_00;
        int mask8 = 0b01_00_00_00_01_00_00_00_01;

        bool player1Win = 
            (match.BoardData & mask1) == mask1 ||
            (match.BoardData & mask2) == mask2 ||
            (match.BoardData & mask3) == mask3 ||
            (match.BoardData & mask4) == mask4 ||
            (match.BoardData & mask5) == mask5 ||
            (match.BoardData & mask6) == mask6 ||
            (match.BoardData & mask7) == mask7 ||
            (match.BoardData & mask8) == mask8;
        
        if (player1Win)
        {
            match.Status = MatchStatus.Finished;
            match.Winner = 1;
            return;
        }
        
        mask1 *= 2;
        mask2 *= 2;
        mask3 *= 2;
        mask4 *= 2;
        mask5 *= 2;
        mask6 *= 2;
        mask7 *= 2;
        mask8 *= 2;

        bool player2Win = 
            (match.BoardData & mask1) == mask1 ||
            (match.BoardData & mask2) == mask2 ||
            (match.BoardData & mask3) == mask3 ||
            (match.BoardData & mask4) == mask4 ||
            (match.BoardData & mask5) == mask5 ||
            (match.BoardData & mask6) == mask6 ||
            (match.BoardData & mask7) == mask7 ||
            (match.BoardData & mask8) == mask8;
            
        if (player2Win)
        {
            match.Status = MatchStatus.Finished;
            match.Winner = 2;
            return;
        }
    }
}