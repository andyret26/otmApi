using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OtmApi.Services.StaffService;
using OtmApi.Services.TournamentService;

namespace OtmApi.Utils;

public class Auth
{
    public async static Task<(bool, string)> IsAuthorized(Claim? tokenSub, IStaffService ss, ITourneyService ts, int tourneyId, string[] roles)
    {
        if (tokenSub == null) return (false, "Unauthorized");
        var osuId = int.Parse(tokenSub.Value.Split("|")[2]);

        var staff = await ss.GetByIdAsync(osuId, tourneyId);
        if (!await ts.StaffsInTourneyAsync(tourneyId, osuId)) return (false, "You do not staff in this tournament");
        if (!staff.Roles.Any(r => roles.Contains(r))) return (false, "You don't have the appropriate role");
        return (true, "");
    }
}