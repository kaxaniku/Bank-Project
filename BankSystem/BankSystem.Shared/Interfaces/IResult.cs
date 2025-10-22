namespace BankSystem.Shared.Interfaces;

public interface IResult<T>
{
    List<string> Messages { get; set; }
    bool Succeeded { get; set; }
    T Data { get; set; }
    int Code { get; set; }
}