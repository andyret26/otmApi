using Microsoft.EntityFrameworkCore;
using OtmApi.Data;
using OtmApi.Data.Entities;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Services.ScheduleService;

public class ScheduleService(DataContext db) : IScheduleService
{
    private readonly DataContext _db = db;


    public async Task<List<QualsSchedule>> GenerateQualsScheduleAsync(int tournamentId, int roundId, DateTime startDate, DateTime endDate)
    {


        var t = await _db.Tournaments.SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        var r = await _db.Rounds.SingleOrDefaultAsync(r => r.Id == roundId);
        if (r == null) throw new NotFoundException("Round", roundId);

        if (_db.QualsSchedules.Any(qs => qs.RoundId == roundId)) throw new AlreadyExistException();

        var startUtc = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, DateTimeKind.Utc);

        var numberOfDays = (endDate - startDate).Days + 1;

        List<QualsSchedule> qualsScheduleList = [];
        var c = 1;
        for (int i = 0; i < numberOfDays * 24; i += 2)
        {
            qualsScheduleList.Add(new QualsSchedule
            {
                DateTime = startUtc.AddHours(i),
                Round = r,
                Num = c.ToString()
            });
            c++;
        }

        await _db.QualsSchedules.AddRangeAsync(qualsScheduleList);
        await _db.SaveChangesAsync();

        var resReturn = await _db.QualsSchedules.Where(qs => qs.RoundId == roundId).ToListAsync();
        return resReturn;
    }

    public Task<List<Schedule>> GenerateScheduleAsync(int tournamentId, int RoundId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<QualsSchedule>> GetQualsScheduleAsync(int roundId)
    {
        if (!await _db.Rounds.AnyAsync(r => r.Id == roundId)) throw new NotFoundException("Round", roundId);
        var test = await _db.QualsSchedules.Where(qs => qs.RoundId == roundId).OrderBy(qs => qs.Id).ToListAsync();
        return test;
    }

    public async Task<QualsSchedule> SetQualsRefereeAsync(int scheduleId, int? refId)
    {
        var qs = await _db.QualsSchedules.SingleOrDefaultAsync(qs => qs.Id == scheduleId);
        if (qs == null) throw new NotFoundException("Quals Schedule", scheduleId);

        if (refId == null)
        {
            qs.Referee = null;
            await _db.SaveChangesAsync();
            return qs;
        }

        var referee = await _db.Staff.SingleOrDefaultAsync(s => s.Id == refId);
        if (referee == null) throw new NotFoundException("Referee", (int)refId);

        qs.Referee = referee.Username;
        await _db.SaveChangesAsync();
        return qs;
    }

    public Task<Staff> SetRefereeAsync(int scheduleId, int? refereeId)
    {
        throw new NotImplementedException();
    }

    public async Task<QualsSchedule> AddNamesToQualsScheduleAsync(int scheduleId, List<string>? names)
    {
        var qs = await _db.QualsSchedules.SingleOrDefaultAsync(qs => qs.Id == scheduleId);
        if (qs == null) throw new NotFoundException("Quals Schedule", scheduleId);
        if (names == null || names.Count == 0) qs.Names = null;
        else qs.Names = names;
        await _db.SaveChangesAsync();
        return qs;
    }

    public async Task<QualsSchedule> AddQualsScheduleAsync(QualsSchedule qualsSchedule)
    {
        var round = await _db.Rounds.SingleOrDefaultAsync(r => r.Id == qualsSchedule.RoundId);
        if (round == null) throw new NotFoundException("Round", qualsSchedule.RoundId);
        qualsSchedule.Round = round;
        await _db.QualsSchedules.AddAsync(qualsSchedule);
        await _db.SaveChangesAsync();
        return qualsSchedule;
    }

    public async Task<QualsSchedule> SetQualsMatchIdAsync(int scheduleId, int? MatchId)
    {
        var qs = await _db.QualsSchedules.SingleOrDefaultAsync(qs => qs.Id == scheduleId);
        if (qs == null) throw new NotFoundException("Quals Schedule", scheduleId);
        qs.MatchId = MatchId;
        await _db.SaveChangesAsync();
        return qs;
    }

}