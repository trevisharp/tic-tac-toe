namespace TicTacToeServer.Models;

public record PlayerStats(
    string Username,
    int FinishedMatches,
    int Wins
);