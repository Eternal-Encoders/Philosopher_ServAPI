using AutoMapper;
using Philosopher_ServAPI.Core.Models.DTOs;
using Philosopher_ServAPI.Core.Models.Entities;

namespace Philosopher_ServAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, GetUserDto>();
        }
    }
}
