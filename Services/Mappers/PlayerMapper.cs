using AutoMapper;
using OtmApi.Data.Dtos;
using OtmApi.Data.Entities;
using OtmApi.Utils;

namespace OtmApi.Services.Mappers
{
    public class PlayerMapper : Profile
    {
        public PlayerMapper()
        {
            CreateMap<PlayerResponseData, Player>()
                .ForMember(player => player.Global_rank, opt => opt.MapFrom(PRD => PRD.Statistics_rulesets.Osu.Global_rank));
            CreateMap<Player, PlayerDto>();
            CreateMap<Player, OtmDashboardPlayerDto>();
        }
    }
}