namespace MyBank.API.Models
{
    public class CountryModel
    {
        public int CountryId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
