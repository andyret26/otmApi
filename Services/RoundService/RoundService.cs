using Microsoft.EntityFrameworkCore;
using OtmApi.Data;
using OtmApi.Data.Entities;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Services.RoundService;

public class RoundService(DataContext db) : IRoundService
{
    private readonly DataContext _db = db;

    public async Task<Round> GetRoundByIdAsync(int id)
    {
        var round = await _db.Rounds.Include(r => r.Mappool).Include(r => r.MapSuggestions).SingleOrDefaultAsync(r => r.Id == id);
        if (round == null) throw new NotFoundException("Round", id);
        return round;
    }
}