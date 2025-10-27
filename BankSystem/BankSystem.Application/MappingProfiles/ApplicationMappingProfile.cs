using AutoMapper;
using BankSystem.Application.Common.DTOs;
using BankSystem.Domain.Entities;

namespace BankSystem.Application.MappingProfiles;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<CreateCustomerRequestDto, Customer>();

        CreateMap<Customer, CreateCustomerResponseDto>();
    }
}