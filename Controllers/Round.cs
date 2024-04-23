using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OtmApi.Data.Dtos;
using OtmApi.Services.RoundService;
using OtmApi.Utils;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Controllers;

[Route("api/v1/round")]
[ApiController]

public class RoundController(IRoundService roundService, IMapper mapper) : ControllerBase
{
    private readonly IRoundService _roundService = roundService;
    private readonly IMapper _mapper = mapper;

    [HttpGet("{id}")]
    public async Task<ActionResult<RoundWithMapsDto>> GetRoundById(int id)
    {
        try
        {
            var round = await _roundService.GetRoundByIdAsync(id);
            return Ok(_mapper.Map<RoundWithMapsDto>(round));

        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("NotFound", 404, e.Message));
        }

    }
}