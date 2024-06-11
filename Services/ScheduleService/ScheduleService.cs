using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OtmApi.Data;
using OtmApi.Data.Entities;
using OtmApi.Utils.Exceptions;
using OtmApi.Utils;

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

    public async Task<List<Schedule>> GenerateScheduleAsync(int tournamentId, int RoundId, DateTime startDate, DateTime endDate)
    {
        if (!await _db.Tournaments.AnyAsync(t => t.Id == tournamentId)) throw new NotFoundException("Tournament", tournamentId);
        var round = await _db.Rounds.SingleOrDefaultAsync(r => r.Id == RoundId);
        if (round == null) throw new NotFoundException("Round", RoundId);

        var isFirstBracket = !await _db.TournamentPlayer.AnyAsync(tp => tp.TournamentId == tournamentId && tp.IsKnockedDown == true);

        if (isFirstBracket)
        {
            var players = await _db.TournamentPlayer.Where(tp => tp.TournamentId == tournamentId && tp.Isknockout == false && tp.Seed != null).Include(tp => tp.Player).OrderBy(tp => tp.Seed).ToListAsync();
            List<Schedule> scheduleList = [];
            for (int i = 0; i < players.Count / 2; i++)
            {
                scheduleList.Add(new Schedule
                {
                    DateTime = Functions.RoundToNearestHour(Functions.GetRandomeDate(startDate, endDate)),
                    Num = i + 1,
                    RoundId = RoundId,
                    Round = round,
                    RoundNumber = 1,
                    Name1 = players[i].Player.Username,
                    Name2 = players[^(i + 1)].Player.Username

                });
            }
            await _db.Schedules.AddRangeAsync(scheduleList);
            await _db.SaveChangesAsync();
        }
        else
        {
            var tournament = await _db.Tournaments.Include(t => t.Rounds).SingleOrDefaultAsync(t => t.Id == tournamentId);
            if (tournament == null) throw new NotFoundException("Tournament", tournamentId);
            var currentRoundIndex = tournament.Rounds!.FindIndex(r => r.Id == RoundId);
            if (currentRoundIndex == -1) throw new NotFoundException("Round", RoundId);
            var previousRoundId = tournament.Rounds[currentRoundIndex - 1].Id;

            var previousRoundSchedules = await _db.Schedules.Where(s => s.RoundId == previousRoundId).ToListAsync();

            List<Schedule> winnerBracketscheduleList = [];
            List<Schedule> loserBracketscheduleList = [];
            for (var i = 0; i < (previousRoundSchedules.Where(prs => prs.IsInLosersBracket == false).Count() / 2); i++)
            {
                winnerBracketscheduleList.Add(new Schedule
                {
                    DateTime = Functions.RoundToNearestHour(Functions.GetRandomeDate(startDate, endDate)),
                    Num = i + 1,
                    RoundId = RoundId,
                    Round = round,
                    RoundNumber = previousRoundSchedules[0].RoundNumber + 1,
                    Name1 = previousRoundSchedules[i].Winner,
                    Name2 = previousRoundSchedules[^(i + 1)].Winner

                });

            }
            // Loosers Bracket
            for (var i = 0; i < previousRoundSchedules.Count / 2; i++)
            {
                if (previousRoundSchedules[0].RoundNumber + 1 == 2)
                {
                    loserBracketscheduleList.Add(new Schedule
                    {
                        DateTime = Functions.RoundToNearestHour(Functions.GetRandomeDate(startDate, endDate)),
                        Num = winnerBracketscheduleList.Count + 1 + i,
                        RoundId = RoundId,
                        Round = round,
                        RoundNumber = 2,
                        Name1 = previousRoundSchedules[0].Loser,
                        Name2 = previousRoundSchedules[^(i + 1)].Loser,
                        IsInLosersBracket = true
                    });
                }
                else
                {
                    var winnerIndex = (previousRoundSchedules.Count / 2) + (i % (previousRoundSchedules.Count / 4));
                    loserBracketscheduleList.Add(new Schedule
                    {
                        DateTime = Functions.RoundToNearestHour(Functions.GetRandomeDate(startDate, endDate)),
                        Num = winnerBracketscheduleList.Count + 1 + i,
                        RoundId = RoundId,
                        Round = round,
                        RoundNumber = previousRoundSchedules[0].RoundNumber + 1,
                        Name1 = previousRoundSchedules[i].Loser,
                        Name2 = previousRoundSchedules[winnerIndex].Winner,

                        IsInLosersBracket = true
                    });

                    var c = loserBracketscheduleList.Count;
                    for (int j = 0; j < c / 2; j++)
                    {
                        loserBracketscheduleList.Add(new Schedule
                        {
                            DateTime = new DateTime(2000, 01, 01),
                            Num = winnerBracketscheduleList.Count + 1 + i,
                            RoundId = RoundId,
                            Round = round,
                            RoundNumber = previousRoundSchedules[0].RoundNumber + 1,
                            Name1 = previousRoundSchedules[j].Winner,
                            Name2 = previousRoundSchedules[^(j + 1)].Winner,

                            IsInLosersBracket = true
                        });

                    }
                }
            }


        }

        return await _db.Schedules.Where(s => s.RoundId == RoundId).ToListAsync();
    }
    public async Task<List<Schedule>> GenerateScheduleTeamAsync(int tournamentId, int RoundId, DateTime startDate, DateTime endDate)
    {
        if (!await _db.Tournaments.AnyAsync(t => t.Id == tournamentId)) throw new NotFoundException("Tournament", tournamentId);
        var round = await _db.Rounds.SingleOrDefaultAsync(r => r.Id == RoundId);
        if (round == null) throw new NotFoundException("Round", RoundId);

        var isFirstBracket = !await _db.Teams.AnyAsync(tt => tt.TournamentId == tournamentId && tt.IsKnockedDown == true);

        if (isFirstBracket)
        {
            var teams = await _db.Teams.Where(tt => tt.TournamentId == tournamentId && tt.Isknockout == false && tt.Seed != null).Include(tt => tt.TeamName).OrderBy(tt => tt.Seed).ToListAsync();
            List<Schedule> scheduleList = [];
            for (int i = 0; i < teams.Count / 2; i++)
            {
                scheduleList.Add(new Schedule
                {
                    DateTime = Functions.RoundToNearestHour(Functions.GetRandomeDate(startDate, endDate)),
                    Num = i + 1,
                    RoundId = RoundId,
                    Round = round,
                    RoundNumber = 1,
                    Name1 = teams[i].TeamName,
                    Name2 = teams[teams.Count - i - 1].TeamName
                });

            }
            await _db.Schedules.AddRangeAsync(scheduleList);
            await _db.SaveChangesAsync();


        }
        throw new NotImplementedException();
    }
}