using AutoMapper;

namespace MyBank.API.Profiles;

public sealed class CardProfile : Profile
{
    public CardProfile()
    {
        CreateMap<Models.CardModel, Domain.Card>().ReverseMap()
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.AccountNumber));
    }
}