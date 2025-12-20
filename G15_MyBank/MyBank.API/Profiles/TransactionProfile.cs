using AutoMapper;

namespace MyBank.API.Profiles;

public sealed class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Models.TransactionModel, Domain.Transaction>().ReverseMap()
            .ForMember(dest => dest.FromAccountNumber, opt => opt.MapFrom(src => src.FromAccount!.AccountNumber))
            .ForMember(dest => dest.ToAccountNumber, opt => opt.MapFrom(src => src.ToAccount!.AccountNumber));
    }
}