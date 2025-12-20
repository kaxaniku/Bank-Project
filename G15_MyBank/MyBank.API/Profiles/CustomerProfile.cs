using AutoMapper;

namespace MyBank.API.Profiles;

public sealed class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Models.CustomerModels.CustomerModel, Domain.Customer>().ReverseMap()
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.City.CityId))
            .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Address.AddressLine1))
            .ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.Address.AddressLine2))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode));
    }
}