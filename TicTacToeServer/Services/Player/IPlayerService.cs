namespace TicTacToeServer.Services.Player;

using Models;

public interface IPlayerService
{
    /// <summary>
    /// Create a user and return you session token.
    /// </summary>
    Task<string?> Create(string username);

    /// <summary>
    /// Recives a user id and returns your statistics.
    /// </summary>
    Task<PlayerStats?> GetStats(Guid id);
}