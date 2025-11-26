using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Infrastructure;

namespace MyBank.RepositoryTest;

public class Seeder
{
    private BankDbContext _context;

    public Seeder(BankDbContext context)
    {
        _context = context;
    }

    public void ClearDatabase()
    {
        _context.Database.ExecuteSqlRaw("DELETE FROM Transactions");
        _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Transactions', RESEED, 0)");

        _context.Database.ExecuteSqlRaw("DELETE FROM Cards");
        _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Cards', RESEED, 0)");

        _context.Database.ExecuteSqlRaw("DELETE FROM Accounts");
        _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Accounts', RESEED, 0)");

        _context.Database.ExecuteSqlRaw("DELETE FROM Customers");
        _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Customers', RESEED, 0)");

        _context.Database.ExecuteSqlRaw("DELETE FROM Cities");
        _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Cities', RESEED, 0)");

        _context.Database.ExecuteSqlRaw("DELETE FROM Countries");
        _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Countries', RESEED, 0)");
    }

    public void SeedDatabase()
    {
        CountrySeedBuilder();
        CitySeedBuilder();
        CustomerSeedBuilder();
        AccountSeedBuilder();
        CardSeedBuilder();
        TransactionSeedBuilder();
    }

    private void AccountSeedBuilder()
    {
        var accounts = AccountBuilder();

        accounts[0].Customer = _context.Customers!.FirstOrDefault(c => c.CustomerId == 1)!;
        accounts[1].Customer = _context.Customers!.FirstOrDefault(c => c.CustomerId == 2)!;
        accounts[2].Customer = _context.Customers!.FirstOrDefault(c => c.CustomerId == 3)!;
        accounts[3].Customer = _context.Customers!.FirstOrDefault(c => c.CustomerId == 4)!;
        accounts[4].Customer = _context.Customers!.FirstOrDefault(c => c.CustomerId == 5)!;

        foreach (var account in accounts)
        {
            _context.Accounts!.Add(account);
        }

        _context.SaveChanges();
    }
    private void CardSeedBuilder()
    {
        var cards = CardBuilder();
        cards[0].Account = _context.Accounts!.FirstOrDefault(a => a.AccountId == 1)!;
        cards[1].Account = _context.Accounts!.FirstOrDefault(a => a.AccountId == 2)!;
        cards[2].Account = _context.Accounts!.FirstOrDefault(a => a.AccountId == 3)!;
        cards[3].Account = _context.Accounts!.FirstOrDefault(a => a.AccountId == 4)!;
        cards[4].Account = _context.Accounts!.FirstOrDefault(a => a.AccountId == 5)!;

        foreach (var card in cards)
        {
            _context.Cards!.Add(card);
        }

        _context.SaveChanges();
    }

    private void CountrySeedBuilder()
    {
        foreach (var country in CountryBuilder())
        {
            _context.Countries!.Add(country);
        }

        _context.SaveChanges();
    }

    private void CitySeedBuilder()
    {
        var cities = CityBuilder();

        cities[0].Country = _context.Countries!.FirstOrDefault(c => c.CountryId == 1)!;
        cities[1].Country = _context.Countries!.FirstOrDefault(c => c.CountryId == 1)!;
        cities[2].Country = _context.Countries!.FirstOrDefault(c => c.CountryId == 2)!;
        cities[3].Country = _context.Countries!.FirstOrDefault(c => c.CountryId == 3)!;
        cities[4].Country = _context.Countries!.FirstOrDefault(c => c.CountryId == 4)!;

        foreach (var city in cities)
        {
            _context.Cities!.Add(city);
        }

        _context.SaveChanges();
    }

    private void CustomerSeedBuilder()
    {
        var customers = CustomerBuilder();
        customers[0].City = _context.Cities!.FirstOrDefault(c => c.CityId == 1)!;
        customers[1].City = _context.Cities!.FirstOrDefault(c => c.CityId == 2)!;
        customers[2].City = _context.Cities!.FirstOrDefault(c => c.CityId == 3)!;
        customers[3].City = _context.Cities!.FirstOrDefault(c => c.CityId == 4)!;
        customers[4].City = _context.Cities!.FirstOrDefault(c => c.CityId == 5)!;

        foreach (var customer in customers)
        {
            _context.Customers!.Add(customer);
        }

        _context.SaveChanges();
    }

    private void TransactionSeedBuilder()
    {
        var transactions = TransactionBuilder();
        transactions[0].FromAccount = _context.Accounts!.FirstOrDefault(a => a.AccountId == 1)!;
        transactions[1].FromAccount = _context.Accounts!.FirstOrDefault(a => a.AccountId == 2)!;
        transactions[2].FromAccount = _context.Accounts!.FirstOrDefault(a => a.AccountId == 3)!;
        transactions[3].FromAccount = _context.Accounts!.FirstOrDefault(a => a.AccountId == 4)!;
        transactions[4].FromAccount = _context.Accounts!.FirstOrDefault(a => a.AccountId == 5)!;

        transactions[0].ToAccount = _context.Accounts!.FirstOrDefault(a => a.AccountId == 5)!;
        transactions[1].ToAccount = _context.Accounts!.FirstOrDefault(a => a.AccountId == 4)!;
        transactions[2].ToAccount = _context.Accounts!.FirstOrDefault(a => a.AccountId == 3)!;
        transactions[3].ToAccount = _context.Accounts!.FirstOrDefault(a => a.AccountId == 2)!;
        transactions[4].ToAccount = _context.Accounts!.FirstOrDefault(a => a.AccountId == 1)!;

        foreach (var transaction in transactions)
        {
            _context.Transactions!.Add(transaction);
        }

        _context.SaveChanges();
    }

    private List<Account> AccountBuilder()
    {
        return new List<Account>
        {
            new Account { AccountNumber = "0000000000000000", Balance = 0, Status = AccountStatus.Active, Activity = new ActivityInfo()},
            new Account { AccountNumber = "0000000000000001", Balance = 10, Status = AccountStatus.Active, Activity = new ActivityInfo()},
            new Account { AccountNumber = "0000000000000002", Balance = 100, Status = AccountStatus.Blocked, Activity = new ActivityInfo()},
            new Account { AccountNumber = "0000000000000003", Balance = 1000, Status = AccountStatus.Active, Activity = new ActivityInfo()},
            new Account { AccountNumber = "0000000000000004", Balance = 10000, Status = AccountStatus.Inactive, Activity = new ActivityInfo()}
        };
    }

    private List<Card> CardBuilder()
    {
        return new List<Card>
        {
            new Card { CardNumber = "0000000000000000", CVC = "000", Status = CardStatus.Active, CardType = CardType.MasterCard, ExpirationDate = DateTime.UtcNow.AddDays(1000), Activity = new ActivityInfo() },
            new Card { CardNumber = "0000000000000001", CVC = "001", Status = CardStatus.Active, CardType = CardType.Visa, ExpirationDate = DateTime.UtcNow.AddDays(1000), Activity = new ActivityInfo() },
            new Card { CardNumber = "0000000000000002", CVC = "002", Status = CardStatus.Inactive, CardType = CardType.MasterCard, ExpirationDate = DateTime.UtcNow.AddDays(1000), Activity = new ActivityInfo() },
            new Card { CardNumber = "0000000000000003", CVC = "003", Status = CardStatus.Active, CardType = CardType.AmericanExpress, ExpirationDate = DateTime.UtcNow.AddDays(1000), Activity = new ActivityInfo() },
            new Card { CardNumber = "0000000000000004", CVC = "004", Status = CardStatus.Suspended, CardType = CardType.MasterCard, ExpirationDate = DateTime.UtcNow.AddDays(1000), Activity = new ActivityInfo() },
        };
    }

    private List<City> CityBuilder()
    {
        return new List<City>
        {
            new City { Name = "Test City 1", Activity = new ActivityInfo() },
            new City { Name = "Test City 2", Activity = new ActivityInfo() },
            new City { Name = "Test City 3", Activity = new ActivityInfo() },
            new City { Name = "Test City 4", Activity = new ActivityInfo() },
            new City { Name = "Test City 5", Activity = new ActivityInfo() }
        };
    }


    private List<Country> CountryBuilder()
    {
        return new List<Country>
        {
            new Country { Name = "Country 1", Code = "Is1", Activity = new ActivityInfo() },
            new Country { Name = "Country 2", Code = "Is2", Activity = new ActivityInfo() },
            new Country { Name = "Country 3", Code = "Is3", Activity = new ActivityInfo() },
            new Country { Name = "Country 4", Code = "Is4", Activity = new ActivityInfo() },
            new Country { Name = "Country 5", Code = "Is5", Activity = new ActivityInfo() }
        };
    }

    private List<Customer> CustomerBuilder()
    {
        return new List<Customer>
        {
            new Customer { PersonalNumber = "00000000000", FirstName = "Name 1", LastName = "LastName 1", Gender = Gender.Male, Email = "ExampleEmail1@gmail.com", PhoneNumber = "995577000000", DateOfBirth = DateTime.Parse("11/4/1918"), Address = new AddressInfo{AddressLine1 = "BankStreet 1", AddressLine2 = "DevStreet 1", ZipCode = "0000"}, Activity = new ActivityInfo() },
            new Customer { PersonalNumber = "00000000001", FirstName = "Name 2", LastName = "LastName 2", Gender = Gender.Female, Email = "ExampleEmail2@gmail.com", PhoneNumber = "995577000001", DateOfBirth = DateTime.Parse("12/4/1950"), Address = new AddressInfo{AddressLine1 = "BankStreet 2", AddressLine2 = "DevStreet 2", ZipCode = "0001"}, Activity = new ActivityInfo() },
            new Customer { PersonalNumber = "00000000002", FirstName = "Name 3", LastName = "LastName 3", Gender = Gender.Male, Email = "ExampleEmail3@gmail.com", PhoneNumber = "995577000002", DateOfBirth = DateTime.Parse("2/2/1960"), Address = new AddressInfo{AddressLine1 = "BankStreet 3", AddressLine2 = "DevStreet 3", ZipCode = "0002"}, Activity = new ActivityInfo() },
            new Customer { PersonalNumber = "00000000003", FirstName = "Name 4", LastName = "LastName 4", Gender = Gender.Female, Email = "ExampleEmail4@gmail.com", PhoneNumber = "995577000003", DateOfBirth = DateTime.Parse("1/1/1965"), Address = new AddressInfo{AddressLine1 = "BankStreet 4", AddressLine2 = "DevStreet 4", ZipCode = "0003"}, Activity = new ActivityInfo() },
            new Customer { PersonalNumber = "00000000004", FirstName = "Name 5", LastName = "LastName 5", Gender = Gender.Other, Email = "ExampleEmail5@gmail.com", PhoneNumber = "995577000004", DateOfBirth = DateTime.Parse("5/7/1941"), Address = new AddressInfo{AddressLine1 = "BankStreet 5", AddressLine2 = "DevStreet 5", ZipCode = "0004"}, Activity = new ActivityInfo() }
        };
    }

    private List<Transaction> TransactionBuilder()
    {
        return new List<Transaction>
        {
            new Transaction { TransactionDate = DateTime.UtcNow.AddDays(1), Description = " To Civillian 1", Amount = 1500, Status = TransactionStatus.Completed},
            new Transaction { TransactionDate = DateTime.UtcNow.AddDays(2), Description = " To Civillian 2", Amount = 750, Status = TransactionStatus.Failed},
            new Transaction { TransactionDate = DateTime.UtcNow.AddDays(3), Description = " To ISIS", Amount = 50000000, Status = TransactionStatus.Blocked},
            new Transaction { TransactionDate = DateTime.UtcNow.AddDays(4), Description = " To Civillian 3", Amount = 14050, Status = TransactionStatus.Completed},
            new Transaction { TransactionDate = DateTime.UtcNow.AddDays(5), Description = " To Hamas", Amount = 400000000, Status = TransactionStatus.Blocked},
        };
    }
}