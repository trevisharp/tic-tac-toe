using System.ComponentModel.DataAnnotations;

namespace TicTacToeServer.Models;

public record UserData(
    [MinLength(3)]
    [RegularExpression("[0-9a-zA-Z _]*")]
    string Username
);