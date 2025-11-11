using AutoMapper;
using BankSystem.Application.Common.DTOs.Account;
using BankSystem.Application.Common.DTOs.Card;
using BankSystem.Application.Common.DTOs.Customer;
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
        CreateMap<Customer, GetCustomerResponseDto>()
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<Account, AccountsDto>()
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<Account, GetAccountResponseDto>()
            .ForMember(dest => dest.CustomerName,
                       opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName))
            .ForMember(dest => dest.Type,
                       opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Status,
                       opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<Card, CardDto>()
            .ForMember(dest => dest.Type,
                       opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Status,
                       opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.CardNumber,
                       opt => opt.MapFrom(src => src.CardNumber));

        CreateMap<CreateAccountRequestDto, Account>();
        CreateMap<Account, CreateAccountResponseDto>();
    }
}