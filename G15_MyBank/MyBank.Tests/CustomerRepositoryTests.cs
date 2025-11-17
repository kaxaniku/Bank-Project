using MyBank.Application.Interfaces.Repositories;
using MyBank.Domain;

namespace MyBank.Tests
{
    public class CustomerRepositoryTests : BaseRepositoryTests<Customer>
    {
        private ICustomerRepository _repository;

        public override void Setup()
        {
            base.Setup();
            _repository = _unitOfWork.CustomerRepository;
        }

        [Test]
        public void TestInsert_ShouldInsert()
        {
            Customer customer = new()
            {
                PersonalNumber = "12345678910",
                FirstName = "Giorgi",
                LastName = "Kapanadze",
                Gender = Gender.Male,
                Email = "Gkkkka222@Gmail.com",
                PhoneNumber = "123456788",
                Address = new AddressInfo
                {
                    AddressLine1 = "123 Test St",
                    AddressLine2 = "Test City",
                    ZipCode = "1234"
                },
                City = _unitOfWork!.CityRepository.GetById(1)!,
                Activity = new ActivityInfo()
            };

            _repository.Insert(customer);
            _unitOfWork.SaveChanges(); 

            Customer insertedCustomer = _repository.GetById(customer.CustomerId)!;
            Assert.IsNotNull(insertedCustomer);
            Assert.That(customer.PersonalNumber, Is.EqualTo(insertedCustomer.PersonalNumber));
            Assert.That(customer.FirstName, Is.EqualTo(insertedCustomer.FirstName));
            Assert.That(customer.LastName, Is.EqualTo(insertedCustomer.LastName));
            Assert.That(customer.Gender, Is.EqualTo(insertedCustomer.Gender));
            Assert.That(customer.PhoneNumber, Is.EqualTo(insertedCustomer.PhoneNumber));
            Assert.That(customer.Email, Is.EqualTo(insertedCustomer.Email));
            Assert.That(customer.Address, Is.EqualTo(insertedCustomer.Address));
            Assert.That(customer.City, Is.EqualTo(insertedCustomer.City));
        }
    }
}
