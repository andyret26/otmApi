using Microsoft.AspNetCore.Mvc;

namespace OtmApi.Controllers;

[Route("/")]
[ApiController]
public class IndexController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Welcome to the OtmApi!");
    }
}
