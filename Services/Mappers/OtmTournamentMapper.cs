using AutoMapper;
using OtmApi.Data.Dtos;
using OtmApi.Data.Entities;


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
        CreateMap<Round, RoundWithMapsDto>();
        CreateMap<TMap, MapDto>();
        CreateMap<TMapSuggestion, MapSuggestionDto>();
    }
}
