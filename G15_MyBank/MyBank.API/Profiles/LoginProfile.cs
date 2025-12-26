using AutoMapper;

namespace MyBank.API.Profiles;

public sealed class LoginProfile : Profile
{
    public LoginProfile()
    {
        CreateMap<Models.UserLogin, Domain.Login>().ReverseMap();
    }
}