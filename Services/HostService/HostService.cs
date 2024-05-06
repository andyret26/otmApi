using Microsoft.EntityFrameworkCore;
using OtmApi.Data;
using Host = OtmApi.Data.Entities.Host;


namespace OmtApi.Services.HostService;

public class HostService : IHostService
{
    private readonly DataContext _db;

    public HostService(DataContext db)
    {
        _db = db;
    }
    public async Task<Host> AddAsync(Host host)
    {
        var addedHost = await _db.Hosts.AddAsync(host);
        await _db.SaveChangesAsync();
        return addedHost.Entity;
    }

    public Task<Host?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public List<Host> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Host?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Host?> UpdateAsync(int id, Host host)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> ExistsAsync(int id)
    {
        return await _db.Hosts.AnyAsync(h => h.Id == id);
    }

    public async Task<bool> HostsTournamentAsync(int osuId, int tournamentId)
    {
        if (await _db.Tournaments.AnyAsync(t => t.Id == tournamentId && t.HostId == osuId)) return true;
        return await _db.Tournaments.Include(t => t.Staff).AnyAsync(t => t.Id == tournamentId && t.Staff!.Any(s => s.Id == osuId && s.Roles.Contains("host")));
    }
}