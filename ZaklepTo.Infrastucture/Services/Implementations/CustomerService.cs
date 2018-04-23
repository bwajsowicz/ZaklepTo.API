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
        public async Task<CustomerDTO> GetAsync(string login)
        {
            var customer = await _customerRepository.GetAsync(login);

            if (customer == null)
                throw new ServiceException(ErrorCodes.CustomerNotFound, "Customer doesn't exist.");

            return _mapper.Map<Customer, CustomerDTO>(customer);
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(customer => _mapper.Map<Customer, CustomerDTO>(customer));
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

        public async Task RegisterAsync(CustomerOnCreateDTO customerDto)
        {
            var customer = await _customerRepository.GetAsync(customerDto.Login);

            if (customer != null)
                throw new ServiceException(ErrorCodes.OwnerAlreadyExists, "Login already in use,");

            var salt = _encrypter.GetSalt(customerDto.Password);
            var hash = _encrypter.GetHash(customerDto.Password, salt);

            var customerToRegister = new Customer(customerDto.Login, customerDto.FirstName, customerDto.LastName, 
                customerDto.Email, customerDto.Phone, hash, salt);

            await _customerRepository.AddAsync(customerToRegister);
        }

        public async Task UpdateAsync(CustomerOnUpdateDTO customerDto)
        {
            var owner = await _customerRepository.GetAsync(customerDto.Login);

            if (owner == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "Customer doesn't exist.");

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
                throw new ServiceException(ErrorCodes.CustomerNotFound, "Customer doesn't exist.");

            var oldPasswordHash = _encrypter.GetHash(passwordChange.OldPassword, customer.Salt);

            if (customer.Password != oldPasswordHash)
                throw new ServiceException(ErrorCodes.InvalidPassword, "Invalid password.");

            var salt = _encrypter.GetSalt(passwordChange.NewPassword);
            var hash = _encrypter.GetHash(passwordChange.NewPassword, salt);

            customer.Password = hash;

            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(string login)
        {
            var customer = await _customerRepository.GetAsync(login);

            if (customer == null)
                throw new ServiceException(ErrorCodes.CustomerNotFound, "Customer doesn't exist.");

            await _customerRepository.DeleteAsync(login);
        }

        public async Task<IEnumerable<RestaurantDTO>> GetMostFrequentRestaurants(string login)
        {
            var reservations = await _reservationRepository.GetAllAsync();

            var topRestaurantsForCustomer = reservations.Where(x => x.Customer.Login == login)
                .GroupBy(x => x.Restaurant)
                .OrderByDescending(x => x.Count())
                .Take(4)
                .Select(x => _mapper.Map<Restaurant, RestaurantDTO>(x.Key));

            return topRestaurantsForCustomer;
        }
    }
}