using AutoMapper;

namespace Products.Services.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<API.Models.GlobalExceptionHandlingMiddleware, DTO.Product>();
        CreateMap<DTO.Product, API.Models.GlobalExceptionHandlingMiddleware>();
    }
}