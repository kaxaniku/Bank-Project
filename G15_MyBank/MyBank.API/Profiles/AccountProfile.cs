using AutoMapper;

namespace MyBank.API.Profiles;

public sealed class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Models.AccountModel, Domain.Account>().ReverseMap()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.CustomerId));
    }
}