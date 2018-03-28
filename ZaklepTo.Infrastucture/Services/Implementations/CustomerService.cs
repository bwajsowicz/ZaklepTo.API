using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastucture.DTO;
using ZaklepTo.Infrastucture.Encrypter;
using ZaklepTo.Core.Exceptions;
using ZaklepTo.Infrastucture.Services.Interfaces;

namespace ZaklepTo.Infrastucture.Services.Implementations
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
            return _mapper.Map<Customer, CustomerDTO>(customer);
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(customer => _mapper.Map<Customer, CustomerDTO>(customer));
        }

        public async Task LoginAsync(string login, string password)
        {
            var customer = await _customerRepository.GetAsync(login);
            if(null==customer)
                throw new ServiceException(ErrorCodes.InvalidLogin, "Login is incorrect."); 

            var hash = _encrypter.GetHash(password, customer.Salt);
            if (customer.Password == hash)
                return;
            throw new ServiceException(ErrorCodes.InvalidPassword, "Password is incorrect.");
        }

        public async Task RegisterAsync(string login, string firstname, string lastname, string email, string phone, string password)
        {
            var customer = await _customerRepository.GetAsync(login);
            if(customer!=null)
                throw new ServiceException(ErrorCodes.CustomerAlreadyExists, "User with that login already exists.");

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);

            customer = new Customer(login, firstname, lastname, email, phone, hash, salt);
            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateAsync(CustomerDTO customerDto)
        {
            var customer = await _customerRepository.GetAsync(customerDto.Login);
            if (null == customer)
                throw new ServiceException(ErrorCodes.CustomerNotFound, "User not found.");

            customer = new Customer(customerDto.Login, customerDto.FirstName, customerDto.LastName, customerDto.Email,
                customerDto.Phone, customer.Password, customer.Salt);

            await _customerRepository.UpdateAsync(customer);
        }

        public async Task ChangePassword(string login, string oldPassword, string newPassword)
        {
            var customer = await _customerRepository.GetAsync(login);
            if(null==customer)
                throw new ServiceException(ErrorCodes.CustomerNotFound, "User not found.");

            var oldPasswordHash = _encrypter.GetHash(oldPassword, customer.Salt);

            if (customer.Password != oldPasswordHash)
                throw new ServiceException(ErrorCodes.InvalidPassword, "Invalid password.");

            var salt = _encrypter.GetSalt(newPassword);
            var hash = _encrypter.GetHash(newPassword, salt);

            customer = new Customer(customer.Login, customer.FirstName, customer.LastName, customer.Email,
                customer.Phone, hash, customer.Salt);

            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(string login)
        {
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