using OtmApi.Data.Entities;

namespace OtmApi.Services.StaffService;

public interface IStaffService
{
    Task<Staff> GetByIdAsync(int id, int tournamentId);
    Task<Staff> AddAsync(Staff staff);
}