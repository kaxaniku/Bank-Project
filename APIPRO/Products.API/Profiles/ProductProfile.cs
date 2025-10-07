using AutoMapper;

namespace Products.API.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Models.Product, DTO.Product>().ReverseMap();
    }
}