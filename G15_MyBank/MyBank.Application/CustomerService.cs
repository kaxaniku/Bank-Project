using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public event Action<Customer>? CustomerCreated;
        public event Action<Customer>? CustomerUpdated;
        public event Action<Customer>? CustomerRemoved;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Customer? FindCustomer(string personalNumber)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(personalNumber));
            
            return _unitOfWork.CustomerRepository.Query(c => c.PersonalNumber.Equals(personalNumber)).FirstOrDefault()!;
        }

        public async Task<Customer?> FindCustomerAsync(string personalNumber, CancellationToken token)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(personalNumber));
            
             return (await _unitOfWork.CustomerRepository.QueryAsync(c => c.PersonalNumber.Equals(personalNumber), token)).FirstOrDefault()!;
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

        public IEnumerable<Customer> GetAllCustomers(int pageNumber, int pageSize)
        {
            return _unitOfWork.CustomerRepository.Query(c => c.Activity.IsActive, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken token, int pageNumber, int pageSize)
        {
            var query =  await _unitOfWork.CustomerRepository.QueryAsync(c => c.Activity.IsActive, token, pageSize, pageNumber);

            return query.ToList();
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
            ArgumentException.ThrowIfNullOrWhiteSpace(personalNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine1);
            ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);
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
            ArgumentException.ThrowIfNullOrWhiteSpace(personalNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine1);
            ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);
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
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(personalNumber));
            
            Customer? customer = _unitOfWork.CustomerRepository.Query(c => c.PersonalNumber.Equals(personalNumber)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(customer);

            _unitOfWork.CustomerRepository.Delete(customer);
            _unitOfWork.SaveChanges();
            OnCustomerRemoved(customer);
        }

        public async Task RemoveCustomerAsync(string personalNumber, CancellationToken token)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(personalNumber));
           
            Customer? customer = (await _unitOfWork.CustomerRepository.QueryAsync(c => c.PersonalNumber.Equals(personalNumber), token)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(customer);

            await _unitOfWork.CustomerRepository.DeleteAsync(customer, token);
            await _unitOfWork.SavechangesAsync(token);
            OnCustomerRemoved(customer);
        }

        public void UpdateCustomer(string personalNumber)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(personalNumber));

            Customer? customer = _unitOfWork.CustomerRepository.Query(c => c.PersonalNumber.Equals(personalNumber)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(customer);

            _unitOfWork.CustomerRepository.Update(customer);
            _unitOfWork.SaveChanges();
            OnCustomerUpdated(customer);
        }

        public async Task UpdateCustomerAsync(string personalNumber, CancellationToken token)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(personalNumber));

            Customer? customer = (await _unitOfWork.CustomerRepository.QueryAsync(c => c.PersonalNumber.Equals(personalNumber), token)).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(customer);

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
            CustomerUpdated?.Invoke(customer);
        }

        private void OnCustomerRemoved(Customer customer)
        {
            CustomerRemoved?.Invoke(customer);
        } 
    }
}
