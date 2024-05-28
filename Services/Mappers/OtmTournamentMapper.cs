using AutoMapper;
using OtmApi.Data.Dtos;
using OtmApi.Data.Entities;
using OtmApi.Utils;


namespace OtmApi.Services.Mappers;
public class OtmMapper : Profile
{
    public OtmMapper()
    {
        CreateMap<PostTourneyDto, Tournament>();
        CreateMap<Tournament, TournamentDto>();
        CreateMap<Staff, StaffDto>();
        CreateMap<Team, TeamDto>();
        CreateMap<Round, RoundDto>();
        CreateMap<Round, RoundDetaildDto>();
        CreateMap<TMap, MapDto>();
        CreateMap<TMap, MapMinDto>();
        CreateMap<TMap, MapWithStatsDto>();
        CreateMap<TMapSuggestion, MapSuggestionDto>();
        CreateMap<TMap, TMapSuggestion>();
        CreateMap<TMapSuggestion, TMap>();
        CreateMap<QualsSchedule, QualsScheduleDto>();
        CreateMap<Tournament, TournamentSimpleDto>();
        CreateMap<TournamentSimpleDto, Tournament>();
        CreateMap<Staff, StaffDto>();
        CreateMap<PlayerStats, PlayerStatsDto>();
        CreateMap<TeamStats, TeamStatsDto>();
        CreateMap<TournamentPlayer, TournamentPlayerDto>();


        CreateMap<Beatmap, TMap>()
            .ForMember(m => m.Artist, opt => opt.MapFrom(b => b.Beatmapset.Artist))
            .ForMember(m => m.Mapper, opt => opt.MapFrom(b => b.Beatmapset.Creator))
            .ForMember(m => m.Image, opt => opt.MapFrom(b => b.Beatmapset.Covers.Cover))
            .ForMember(m => m.Name, opt => opt.MapFrom(b => b.Beatmapset.Title));
        CreateMap<Beatmap, TMapSuggestion>()
            .ForMember(m => m.Artist, opt => opt.MapFrom(b => b.Beatmapset.Artist))
            .ForMember(m => m.Mapper, opt => opt.MapFrom(b => b.Beatmapset.Creator))
            .ForMember(m => m.Image, opt => opt.MapFrom(b => b.Beatmapset.Covers.Cover))
            .ForMember(m => m.Name, opt => opt.MapFrom(b => b.Beatmapset.Title));
    }
}
