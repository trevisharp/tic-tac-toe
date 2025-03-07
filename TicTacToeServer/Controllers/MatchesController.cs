using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TicTacToeServer.Controllers;

using Models;
using Services.Matches;
using TicTacToeServer.Entities;

[ApiController]
public class MatchesController(IMatchesService service) : ControllerBase
{
    [HttpPost("new")]
    [Authorize]
    public async Task<IActionResult> New()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim is null)
            return Forbid();
        
        if (!Guid.TryParse(claim.Value, out var id))
            return Forbid();
            
        var match = await service.FindPair(id);
        
        if (match.Status == Entities.MatchStatus.Pairing)
            return Ok(new { message = "Buscando partida..."});
        
        return Ok(new { message = "Partida encontrada!"});
    }

    [HttpGet("match")]
    [Authorize]
    public async Task<IActionResult> GetMatches(
        [FromQuery] string status = "all",
        [FromQuery] int page = 1,
        [FromQuery] int limit = 12
    )
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        var nameClaim = User.FindFirst(ClaimTypes.Name);
        if (claim is null)
            return Forbid();
        
        if (!Guid.TryParse(claim.Value, out var id))
            return Forbid();
        
        var matches = await service.Find(id, status, page, limit);
        return Ok(
            matches.Select(m => {
                var player1 = m.Player1?.Username ?? "unknow";
                var player2 = m.Player2?.Username ?? "unknow";
                var winner = m.Winner switch {
                    1 => player1,
                    2 => player2,
                    _ => null
                };
                var active = m.ActivePlayer switch {
                    1 => player1,
                    2 => player2,
                    _ => null
                };
                var status = m.Status switch {
                    MatchStatus.Finished => "finished",
                    MatchStatus.Pairing => "pairing",
                    MatchStatus.InGame => "in game",
                    _ => "unknow"
                };

                var game = new List<int>();
                int boardData = m.BoardData;
                while (game.Count < 9) {
                    game.Add(boardData % 4);
                    boardData <<= 2;
                }

                return new MatchData(
                    m.Id, player1, player2,
                    active, status, winner, [ ..game ], m.PlayTime,
                    nameClaim?.Value == active
                );
            })
        );
    }

    [HttpGet("match/{id}")]
    public async Task<IActionResult> GetMatch(Guid id)
    {
        var match = await service.FindOne(id);
        if (match is null)
            return NotFound();

        var player1 = match.Player1?.Username ?? "unknow";
        var player2 = match.Player2?.Username ?? "unknow";
        var winner = match.Winner switch {
            1 => player1,
            2 => player2,
            _ => null
        };
        var active = match.ActivePlayer switch {
            1 => player1,
            2 => player2,
            _ => null
        };
        var status = match.Status switch {
            MatchStatus.Finished => "finished",
            MatchStatus.Pairing => "pairing",
            MatchStatus.InGame => "in game",
            _ => "unknow"
        };

        var game = new List<int>();
        int boardData = match.BoardData;
        while (game.Count < 9) {
            game.Add(boardData % 4);
            boardData <<= 2;
        }

        var data = new MatchData(
            match.Id, player1, player2,
            active, status, winner, [ ..game ], match.PlayTime,
            false
        );
        
        return Ok(data);
    }

    [HttpPost("play")]
    [Authorize]
    public async Task<IActionResult> Play([FromBody]PlayData playData)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim is null)
            return Forbid();
        
        if (!Guid.TryParse(claim.Value, out var id))
            return Forbid();
        
        return Ok(await service.Play(id, playData.GameId, playData.Play));
    }
}