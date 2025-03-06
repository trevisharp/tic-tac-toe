namespace TicTacToeServer.Entities;

public class Match
{
    public Guid Id { get; set; }
    public MatchStatus Status { get; set; }
    public ApplicationUser? Player1 { get; set; }
    public ApplicationUser? Player2 { get; set; }
    public int Winner { get; set; }
    public int BoardData { get; set; }
    public DateTime PlayTime { get; set; }
    public int ActivePlayer { get; set; }
}