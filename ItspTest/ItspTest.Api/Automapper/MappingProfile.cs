using AutoMapper;
using ItspTest.Api.Dtos;
using ItspTest.Core.Models;

namespace ItspTest.Api.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCollection, MovieCollectionDto>();
            CreateMap<Movie, MovieDto>();
        }
    }
}