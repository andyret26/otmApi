using OtmApi.Data.Entities;

namespace OtmApi.Services.ScheduleService;

public interface IScheduleService
{
    Task<List<QualsSchedule>> GenerateQualsScheduleAsync(int tournamentId, int RoundId, DateTime startDate, DateTime endDate);

    Task<List<QualsSchedule>> GetQualsScheduleAsync(int roundId);
    Task<QualsSchedule> SetQualsRefereeAsync(int scheduleId, int? refereeId);
    Task<QualsSchedule> SetQualsMatchIdAsync(int scheduleId, int? MatchId);
    Task<Staff> SetRefereeAsync(int scheduleId, int? refereeId);

    Task<QualsSchedule> AddNamesToQualsScheduleAsync(int scheduleId, List<string>? names);
    Task<QualsSchedule> AddQualsScheduleAsync(QualsSchedule qualsSchedule);
    Task<List<Schedule>> GenerateScheduleAsync(int tournamentId, int RoundId, DateTime startDate, DateTime endDate);
    Task<List<Schedule>> GenerateScheduleTeamAsync(int tournamentId, int RoundId, DateTime startDate, DateTime endDate);
}