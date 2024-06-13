namespace OtmApi.Services.Apis;

public interface IChallongeApiService
{
    Task CreateTournamentAsync(int tournamentId, string tournamentName);
    Task AddParticipantsBulkAsync(int tournamentId);
}