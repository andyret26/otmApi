using Microsoft.EntityFrameworkCore;
using OtmApi.Data;
using OtmApi.Data.Entities;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Services.StaffService;

public class StaffService(DataContext db) : IStaffService
{
    private readonly DataContext _db = db;

    public async Task<Staff> AddAsync(Staff staff)
    {
        var addedStaff = await _db.Staff.AddAsync(staff);
        await _db.SaveChangesAsync();
        return addedStaff.Entity;
    }

    public async Task<Staff> GetByIdAsync(int id, int tournamentId)
    {
        var staff = await _db.Staff.SingleOrDefaultAsync(s => s.Id == id && s.TournamentId == tournamentId);
        if (staff == null) throw new NotFoundException("Staff | Tournament", decimal.Parse($"{id}.{tournamentId}"));
        return staff;
    }


}
