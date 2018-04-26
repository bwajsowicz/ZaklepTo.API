using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Exceptions;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.Encrypter;
using ZaklepTo.Infrastructure.Services.Interfaces;

namespace ZaklepTo.Infrastructure.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IEncrypter encrypter, IReservationRepository reservationRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _encrypter = encrypter;
            _reservationRepository = reservationRepository;
        }
        public async Task<CustomerDto> GetAsync(string customersLogin)
        {
            var customerToGet = await _customerRepository.GetAsync(customersLogin);

            if (customerToGet == null)
                throw new ServiceException(ErrorCodes.CustomerNotFound, "CustomerEntity doesn't exist.");

            return _mapper.Map<Customer, CustomerDto>(customerToGet);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(customer => _mapper.Map<Customer, CustomerDto>(customer));
        }

        public async Task LoginAsync(LoginCredentials loginCredentials)
        {
            var customer = await _customerRepository.GetAsync(loginCredentials.Login);

            if(customer == null)
                throw new ServiceException(ErrorCodes.CustomerNotFound, "Login doesn't match any account.");

            var hash = _encrypter.GetHash(loginCredentials.Password, customer.Salt);

            if (customer.Password == hash)
                return;

            throw new ServiceException(ErrorCodes.InvalidPassword, "Password is incorrect.");
        }

        public async Task RegisterAsync(CustomerOnCreateDto customerDto)
        {
            var customer = await _customerRepository.GetAsync(customerDto.Login);

            if (customer != null)
                throw new ServiceException(ErrorCodes.OwnerAlreadyExists, "Login is already in use.");

            var salt = _encrypter.GetSalt(customerDto.Password);
            var hash = _encrypter.GetHash(customerDto.Password, salt);

            var customerToRegister = new Customer(customerDto.Login, customerDto.FirstName, customerDto.LastName, 
                customerDto.Email, customerDto.Phone, hash, salt);

            await _customerRepository.AddAsync(customerToRegister);
        }

        public async Task UpdateAsync(CustomerOnUpdateDto customerDto)
        {
            var customerToUpdate = await _customerRepository.GetAsync(customerDto.Login);

            if (customerToUpdate == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "CustomerEntity doesn't exist.");

            var customer = await _customerRepository.GetAsync(customerDto.Login);

            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.Email = customerDto.Email;
            customer.Phone = customerDto.Phone;

            await _customerRepository.UpdateAsync(customer);
        }

        public async Task ChangePassword(PasswordChange passwordChange)
        {
            var customer = await _customerRepository.GetAsync(passwordChange.Login);

            if(customer == null)
                throw new ServiceException(ErrorCodes.CustomerNotFound, "CustomerEntity doesn't exist.");

            var oldPasswordHash = _encrypter.GetHash(passwordChange.OldPassword, customer.Salt);

            if (customer.Password != oldPasswordHash)
                throw new ServiceException(ErrorCodes.InvalidPassword, "Invalid password.");

            var salt = _encrypter.GetSalt(passwordChange.NewPassword);
            var hash = _encrypter.GetHash(passwordChange.NewPassword, salt);

            customer.Password = hash;

            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(string customersLogin)
        {
            var customerToDelete = await _customerRepository.GetAsync(customersLogin);

            if (customerToDelete == null)
                throw new ServiceException(ErrorCodes.CustomerNotFound, "CustomerEntity doesn't exist.");

            await _customerRepository.DeleteAsync(customersLogin);
        }

        public async Task<IEnumerable<RestaurantDto>> GetMostFrequentRestaurants(string login)
        {
            var customer = await _customerRepository.GetAsync(login);

            if (customer == null)
                throw new ServiceException(ErrorCodes.CustomerNotFound, "CustomerEntity doesn't exist.");

            var reservations = await _reservationRepository.GetAllAsync();

            var topRestaurantsForCustomer = reservations.Where(x => x.Customer.Login == login)
                .GroupBy(x => x.Restaurant)
                .OrderByDescending(x => x.Count())
                .Take(4)
                .Select(x => _mapper.Map<Restaurant, RestaurantDto>(x.Key));

            return topRestaurantsForCustomer;
        }
    }
}