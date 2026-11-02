using Microsoft.AspNetCore.Mvc;
using OtmApi.Services.Discord;
using OtmApi.Services.HostService;

namespace OtmApi.Controllers;

[Route("/")]
[ApiController]
public class IndexController(IHostService hostService, IDiscordService disc) : ControllerBase
{
    private readonly IHostService _hostService = hostService;
    private readonly IDiscordService _disc = disc;

    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Welcome to the OtmApi!");
    }

    [HttpGet("ping")]
    public async Task<IActionResult> Ping()
    {
        await _hostService.GetByIdAsync(3191010);

        return Ok("Pong");
    }

    [HttpGet("ping-discord")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> PingDiscord(){
        await _disc.SendMessage($"pinged from OTM server");
        return Ok("Pinged discord OK");
    }
}
