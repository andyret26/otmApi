namespace OtmApi.Services.Apis;

public interface IChallongeApiService
{
    Task CreateTournamentAsync(int tournamentId);
    Task AddParticipantsBulkAsync(int tournamentId);

    Task GetRoundMatchesAsync(int tournamentId, int roundNum);
}