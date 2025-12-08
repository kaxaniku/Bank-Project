using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public event Action<Customer>? CustomerCreated;
        public event Action<Customer>? Customerupdated;
        public event Action<Customer>? CustomerRemoved;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Customer? FindCustomer(string personalNumber)
        {
            Customer customer = _unitOfWork.CustomerRepository.Query(c => c.PersonalNumber.Equals(personalNumber)).FirstOrDefault()!;

            ArgumentNullException.ThrowIfNullOrWhiteSpace(personalNumber, $"Customer with number {personalNumber} not found");

            return customer;
        }

        public async Task<Customer?> FindCustomerAsync(string personalNumber, CancellationToken token)
        {
            Customer customer = (await _unitOfWork.CustomerRepository.QueryAsync(c => c.PersonalNumber.Equals(personalNumber), token)).FirstOrDefault()!;

            ArgumentNullException.ThrowIfNullOrWhiteSpace(personalNumber, $"Customer with number {personalNumber} not found");

            return customer;
        }

        public IEnumerable<Account> GetAccountsByCustomer(string personalNumber)
        {
            return _unitOfWork.AccountRepository.Query(a => a.Customer.PersonalNumber.Equals(personalNumber)).ToList();
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerAsync(string personalNumber, CancellationToken token)
        {
            var accounts = await _unitOfWork.AccountRepository.QueryAsync(a => a.Customer.PersonalNumber.Equals(personalNumber), token);

            return accounts.ToList();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _unitOfWork.CustomerRepository.Query(c => c.Activity.IsActive).ToList();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken token)
        {
            return await _unitOfWork.CustomerRepository.QueryAsync(c => c.Activity.IsActive, token);
        }

        public void RegisterNewCustomer(string personalNumber, 
            string firstName, 
            string lastName, 
            Gender gender, 
            string email,
            string phoneNumber,
            DateTime dateOfBirth,
            string addressLine1,
            string? addressLine2,
            string zipCode,
            int cityId)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(personalNumber);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(email);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(phoneNumber);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(addressLine1);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(zipCode);
            if (dateOfBirth >= DateTime.UtcNow) throw new ArgumentException("Date of birth must be in the past.", nameof(dateOfBirth));

            City city = _unitOfWork.CityRepository.GetById(cityId)!;

            Customer customer = new()
            {
                PersonalNumber = personalNumber,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Email = email,
                PhoneNumber = personalNumber,
                DateOfBirth = dateOfBirth,
                Address = new()
                {
                    AddressLine1 = addressLine1,
                    AddressLine2 = addressLine2,
                    ZipCode = zipCode
                },
                City = city

            };

            _unitOfWork.CustomerRepository.Insert(customer);
            _unitOfWork.SaveChanges();
            OnCustomerCreated(customer);
        }

        public async Task RegisterNewCustomerAsync(string personalNumber,
            string firstName,
            string lastName,
            Gender gender,
            string email,
            string phoneNumber,
            DateTime dateOfBirth,
            string addressLine1,
            string? addressLine2,
            string zipCode,
            int cityId,
            CancellationToken token)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(personalNumber);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(email);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(phoneNumber);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(addressLine1);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(zipCode);
            if (dateOfBirth >= DateTime.UtcNow) throw new ArgumentException("Date of birth must be in the past.", nameof(dateOfBirth));

            City city = await _unitOfWork.CityRepository.GetByIdAsync(cityId, token)!;

            Customer customer = new()
            {
                PersonalNumber = personalNumber,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Email = email,
                PhoneNumber = personalNumber,
                DateOfBirth = dateOfBirth,
                Address = new()
                {
                    AddressLine1 = addressLine1,
                    AddressLine2 = addressLine2,
                    ZipCode = zipCode
                },
                City = city

            };

            await _unitOfWork.CustomerRepository.InsertAsync(customer, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCustomerCreated(customer);
        }

        public void RemoveCustomer(string personalNumber)
        {
            Customer customer = _unitOfWork.CustomerRepository.Query(c => c.PersonalNumber.Equals(personalNumber)).FirstOrDefault()!;

            ArgumentNullException.ThrowIfNullOrWhiteSpace(personalNumber, $"Customer with number {personalNumber} not found");

            _unitOfWork.CustomerRepository.Delete(customer);
            _unitOfWork.SaveChanges();
            OnCustomerRemoved(customer);
        }

        public async Task RemoveCustomerAsync(string personalNumber, CancellationToken token)
        {
            var customer = (await _unitOfWork.CustomerRepository.QueryAsync(c => c.PersonalNumber.Equals(personalNumber), token)).FirstOrDefault();

            ArgumentNullException.ThrowIfNullOrWhiteSpace(personalNumber, $"Customer with personal number {personalNumber} not found");

            await _unitOfWork.CustomerRepository.DeleteAsync(customer, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCustomerRemoved(customer);
        }

        public void UpdateCustomer(string personalNumber)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(personalNumber, $"Customer with personal number {personalNumber} not found");

            Customer customer = _unitOfWork.CustomerRepository.Query(c => c.PersonalNumber.Equals(personalNumber)).FirstOrDefault()!;

            _unitOfWork.CustomerRepository.Update(customer);
            _unitOfWork.SaveChanges();
            OnCustomerUpdated(customer);
        }

        public async Task UpdateCustomerAsync(string personalNumber, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(personalNumber, $"Customer with personal number {personalNumber} not found");

            Customer customer = (await _unitOfWork.CustomerRepository.QueryAsync(c => c.PersonalNumber.Equals(personalNumber), token)).FirstOrDefault()!;

            await _unitOfWork.CustomerRepository.UpdateAsync(customer, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCustomerUpdated(customer);
        }

        private void OnCustomerCreated(Customer customer)
        {
            CustomerCreated?.Invoke(customer);
        }

        private void OnCustomerUpdated(Customer customer)
        {
            Customerupdated?.Invoke(customer);
        }

        private void OnCustomerRemoved(Customer customer)
        {
            CustomerRemoved?.Invoke(customer);
        } 
    }
}
