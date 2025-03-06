namespace TicTacToeServer.Entities;

public class ApplicationUser
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
}