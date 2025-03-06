using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TicTacToeServer.Controllers;

using Models;
using Services.Matches;

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
        if (claim is null)
            return Forbid();
        
        if (!Guid.TryParse(claim.Value, out var id))
            return Forbid();
        
        return Ok(await service.Find(id, status, page, limit));
    }

    [HttpGet("match/{id}")]
    public async Task<IActionResult> GetMatch(Guid id)
    {
        var match = await service.FindOne(id);
        if (match is null)
            return NotFound();
        
        return Ok(match);
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