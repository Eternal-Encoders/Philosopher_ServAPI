using AutoMapper;
using Philosopher_ServAPI.Core.Models.DTOs;
using Philosopher_ServAPI.Core.Models.DTOs.Game.Card;
using Philosopher_ServAPI.Core.Models.DTOs.Game.Level;
using Philosopher_ServAPI.Core.Models.Entities;
using Philosopher_ServAPI.Core.Models.Entities.Game;

namespace Philosopher_ServAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<PostCardDto, Card>();
            CreateMap<PostLevelDto, Level>();
        }
    }
}
