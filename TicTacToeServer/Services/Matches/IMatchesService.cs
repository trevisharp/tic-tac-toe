namespace TicTacToeServer.Services.Matches;

using Models;
using Entities;

public interface IMatchesService
{
    /// <summary>
    /// Search a player matches based on status with pagination.
    /// </summary>
    Task<List<Match>> Find(Guid playerId, string status, int page, int limit);
    
    /// <summary>
    /// Find a specific match.
    /// </summary>
    Task<Match?> FindOne(Guid matchId);

    /// <summary>
    /// Create a new match.
    /// </summary>
    Task<Match> FindPair(Guid playerId);

    /// <summary>
    /// Make a play on a specific match using play index on the board.
    /// </summary>
    Task<PlayResult?> Play(Guid id, Guid matchId, int playCode);
}