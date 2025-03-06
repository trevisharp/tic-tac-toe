using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToeServer.Controllers;

using Models;
using Services.Player;

[ApiController]
public class PlayerController(IPlayerService service) : ControllerBase
{
    [HttpPost("newSession")]
    public async Task<IActionResult> NewSession([FromBody]UserData userData)
    {
        var token = await service.Create(userData.Username);
        if (token is null)
            return BadRequest();
        
        return Ok(new { token });
    }

    [HttpGet("player")]
    [Authorize]
    public async Task<IActionResult> GetStats()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim is null)
            return Forbid();

        return Guid.TryParse(claim.Value, out var id) ?
            Ok (await service.GetStats(id)) :
            Forbid();
    }
}