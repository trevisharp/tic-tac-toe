namespace TicTacToeServer.Models;

public record MatchData(
    Guid Id,
    string Player1,
    string Player2,
    string? Active,
    string Status,
    string? Winnner,
    int[]? Game,
    DateTime? PlayerTime
);