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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;

        public EmployeeService(IEmployeeRepository employeeRepository, IRestaurantRepository restaurantRepository, IMapper mapper, IEncrypter encrypter)
        {
            _employeeRepository = employeeRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<EmployeeDTO> GetAsync(string login)
        {
            var employee = await _employeeRepository.GetAsync(login);

            if (employee == null)
                throw new ServiceException(ErrorCodes.EmployeeNotFound, "Employee doesn't exist.");

            return _mapper.Map<Employee, EmployeeDTO>(employee);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees.Select(employee => _mapper.Map<Employee, EmployeeDTO>(employee));
        }

        public async Task LoginAsync(LoginCredentials loginCredentials)
        {
            var employee = await _employeeRepository.GetAsync(loginCredentials.Login);

            if (employee == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "Login doesn't match any account.");

            var hash = _encrypter.GetHash(loginCredentials.Password, employee.Salt);

            if(employee.Password == hash)
                return;

            throw new ServiceException(ErrorCodes.InvalidPassword, "Password is incorrect.");
        }

        public async Task RegisterAsync(EmployeeOnCreateDTO employeeDto)
        {
            var employee = await _employeeRepository.GetAsync(employeeDto.Login);

            if (employee != null)
                throw new ServiceException(ErrorCodes.OwnerAlreadyExists, "Login already in use.");

            var salt = _encrypter.GetSalt(employeeDto.Password);
            var hash = _encrypter.GetHash(employeeDto.Password, salt);

            var restaurant = await _restaurantRepository.GetAsync(employeeDto.Restaurant.Id);

            var employeeToRegister = new Employee(employeeDto.Login, employeeDto.FirstName, employeeDto.LastName,
                employeeDto.Email, employeeDto.Phone, hash, salt, restaurant);

            await _employeeRepository.AddAsync(employeeToRegister);
        }

        public async Task UpdateAsync(EmployeeOnUpdateDTO employeeDto)
        {
            var owner = await _employeeRepository.GetAsync(employeeDto.Login);

            if (owner == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "Employee doesn't exist.");

            var employee = await _employeeRepository.GetAsync(employeeDto.Login);

            employee.LastName = employeeDto.LastName;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;

            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task ChangePassword(PasswordChange passwordChange)
        {
            var employee = await _employeeRepository.GetAsync(passwordChange.Login);

            if (employee == null)
                throw new ServiceException(ErrorCodes.CustomerNotFound, "Customer doesn't exist.");

            var oldPasswordHash = _encrypter.GetHash(passwordChange.OldPassword, employee.Salt);

            if (employee.Password != oldPasswordHash)
                throw new ServiceException(ErrorCodes.InvalidPassword, "Invalid password.");

            var salt = _encrypter.GetSalt(passwordChange.NewPassword);
            var hash = _encrypter.GetHash(passwordChange.NewPassword, salt);

            employee.Password = hash;

            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteAsync(string login)
        {
            var employee = await _employeeRepository.GetAsync(login);

            if (employee == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "Employee doesn't exist.");

            await _employeeRepository.DeleteAsync(login);
        }
    }
}