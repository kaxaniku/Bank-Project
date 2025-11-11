using AutoMapper;
using BankSystem.Application.Common.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace BankSystem.Infrastructure.MappingProfiles;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<IdentityUser, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));
    }
}
