using TicTacToeServer.Entities;

namespace TicTacToeServer.Services.Matches;

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
    Task<Match?> FindPair(Guid playerId);
}