using Host = OtmApi.Data.Entities.Host;


namespace OmtApi.Services.HostService;

public interface IHostService
{
    Task<Host> AddAsync(Host host);
    Task<Host?> GetByIdAsync(int id);
    List<Host> GetAsync();
    Task<Host?> DeleteAsync(int id);
    Task<Host?> UpdateAsync(int id, Host host);

    /// <summary>
    /// Check if a host exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns>True if host exists, otherwise returns false</returns>
    Task<bool> ExistsAsync(int id);

    /// <summary>
    /// Check if a osuId is hosting or co-hosting a tournament
    /// </summary>
    /// <param name="hostId"></param>
    /// <param name="tournamentId"></param>
    /// <returns></returns>
    Task<bool> HostsTournamentAsync(int hostId, int tournamentId);

}