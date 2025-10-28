using AutoMapper;
using BankSystem.Application.Common.DTOs;
using BankSystem.Domain.Entities;

namespace BankSystem.Application.MappingProfiles;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<CreateCustomerRequestDto, Customer>();

        CreateMap<UpdateCustomerRequestDto, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.NationalId, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore());

        CreateMap<Customer, CustomerResponseDto>();

        CreateMap<Customer, GetCustomerResponseDto>();
    }
}