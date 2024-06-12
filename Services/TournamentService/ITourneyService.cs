using OtmApi.Data.Dtos;
using OtmApi.Data.Entities;

namespace OtmApi.Services.TournamentService;

public interface ITourneyService
{
    public Task<Tournament> AddAsync(Tournament tournament);
    public Task<List<Tournament>> GetAsync();
    public Task<Tournament?> GetByIdAsync(int id);
    public Task<Tournament> GetByIdSimpleAsync(int id);
    public Task<Tournament?> UpdateAsync(Tournament tournament);
    public Task DeleteAsync(int id);

    /// <summary>
    /// Get all tournaments created by the spcified host
    /// </summary>
    /// <param name="hostId">Host to get tournaments from</param>
    /// <returns>List of tournaments created by the specified host</returns>
    public Task<List<Tournament>> GetAllByHostIdAsync(int hostId);

    public Task AddChallongeIdAsync(int tournamentId, int challongeId);
    public Task<Tournament> AddTeamAsync(int tournamentId, Team team);
    public Task<Player> AddPlayerAsync(int tournamentId, Player player);
    public Task<Round> AddRoundAsync(int tournamentId, Round round);
    public Task<bool> TeamNameExistsInTournamentAsync(int tournamentId, string teamName);
    public Task<bool> PlayerExistsInTourneyAsync(int tournamentId, int osuId);
    /// <summary>
    /// Check if a player exists in a team in the specified tournament
    /// </summary>
    /// <param name="tournamentId">Tournament to check in</param>
    /// <param name="playerIds">List of player ids to check</param>
    /// <returns>List of player ids that has a team</returns>
    public Task<List<int>> PlayerExistsInTeamTournamentAsync(int tournamentId, List<int> playerIds);

    /// <summary>
    /// Check if a staff exists in a tournament
    /// </summary>
    /// <param name="tournamentId"></param>
    /// <param name="staffId"></param>
    /// <returns></returns>
    public Task<bool> StaffsInTourneyAsync(int tournamentId, int staffId);
    public Task<List<Player>> GetAllPlayersAsync(int tournamentId);
    public Task<List<Team>> GetAllTeamsAsync(int tournamentId);
    public Task<List<Staff>> GetAllStaffsAsync(int tournamentId);
    public Task<bool> IsTeamTourneyAsync(int tournamentId);

    public Task SetPlayerSeedsAsync(int tournamentId, List<PlayerSeedDto> players);
    public Task SetTeamSeedsAsync(int tournamentId, List<TeamSeedDto> teams);
    public Task SetHowManyQualifiesAsync(int tournamentId, string howManyQualifies);
    public Task KnockoutPlayersAsync(int tournamentId, List<int> playerIds);
    public Task KnockoutTeamsAsync(int tournamentId, List<int> teamIds);
}
