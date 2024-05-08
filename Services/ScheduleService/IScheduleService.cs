using OtmApi.Data.Entities;

namespace OtmApi.Services.ScheduleService;

public interface IScheduleService
{
    Task<List<QualsSchedule>> GenerateQualsScheduleAsync(int tournamentId, int RoundId, DateTime startDate, DateTime endDate);
    Task<List<Schedule>> GenerateScheduleAsync(int tournamentId, int RoundId);

    Task<List<QualsSchedule>> GetQualsScheduleAsync(int roundId);
    Task<Staff> SetQualsRefereeAsync(int scheduleId, int refereeId);
    Task<Staff> SetRefereeAsync(int scheduleId, int refereeId);
}