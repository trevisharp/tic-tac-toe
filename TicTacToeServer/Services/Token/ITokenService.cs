namespace TicTacToeServer.Services.Token;

public interface ITokenService
{
    string Create(Guid id, string username);
}