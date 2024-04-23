using OtmApi.Data.Entities;

namespace OtmApi.Services.RoundService;

public interface IRoundService
{
    public Task<Round> GetRoundByIdAsync(int id);
}