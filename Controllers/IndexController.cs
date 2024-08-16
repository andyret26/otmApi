using Microsoft.AspNetCore.Mvc;
using OtmApi.Services.HostService;

namespace OtmApi.Controllers;

[Route("/")]
[ApiController]
public class IndexController(IHostService hostService) : ControllerBase
{
    private readonly IHostService _hostService = hostService;

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
}
