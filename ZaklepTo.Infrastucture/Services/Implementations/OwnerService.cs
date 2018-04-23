using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;

        public OwnerService(IOwnerRepository ownerRepository, IRestaurantRepository restaurantRepository, IMapper mapper, IEncrypter encrypter)
        {
            _ownerRepository = ownerRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task ChangePassword(PasswordChange passwordChange)
        {
            var owner = await _ownerRepository.GetAsync(passwordChange.Login);

            var oldPasswordHash = _encrypter.GetHash(passwordChange.OldPassword, owner.Salt);

            if (owner.Password != oldPasswordHash)
                throw new ServiceException(ErrorCodes.InvalidPassword, "Invalid password.");

            var salt = _encrypter.GetSalt(passwordChange.NewPassword);
            var hash = _encrypter.GetHash(passwordChange.NewPassword, salt);

            owner.Password = hash;

            await _ownerRepository.UpdateAsync(owner);
        }

        public async Task DeleteAsync(string login)
        {
            var owner = await _ownerRepository.GetAsync(login);

            if (owner == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "Owner with given login doesn't exist.");

            await _ownerRepository.DeleteAsync(login);
        }

        public async Task<IEnumerable<OwnerDTO>> GetAllAsync()
        {
            var owners = await _ownerRepository.GetAllAsync();
            return owners.Select(owner => _mapper.Map<Owner, OwnerDTO>(owner));
        }

        public async Task<OwnerDTO> GetAsync(string login)
        {
            var owner = await _ownerRepository.GetAsync(login);

            if(owner == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "Owner doesn't exist.");

            return _mapper.Map<Owner, OwnerDTO>(owner);
        }

        public async Task LoginAsync(LoginCredentials loginCredentials)
        {
            var owner = await _ownerRepository.GetAsync(loginCredentials.Login);

            if(owner == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "Login doesn't match any account.");

            var hash = _encrypter.GetHash(loginCredentials.Password, owner.Salt);

            if (owner.Password == hash)
                return;
            else
                throw new ServiceException(ErrorCodes.InvalidPassword, "Password is incorrect.");
        }

        public async Task RegisterAsync(OwnerOnCreateDTO ownerDto)
        {
            var owner = await _ownerRepository.GetAsync(ownerDto.Login);

            if (owner != null)
                throw new ServiceException(ErrorCodes.OwnerAlreadyExists, "Login already in use.");

            var salt = _encrypter.GetSalt(ownerDto.Password);
            var hash = _encrypter.GetHash(ownerDto.Password, salt);
            
            var ownersRestaurant = await _restaurantRepository.GetAsync(ownerDto.Restaurant.Id);

            var ownerToRegister = new Owner(ownerDto.Login, ownerDto.FirstName, ownerDto.LastName, ownerDto.Email, 
                ownerDto.Phone, ownerDto.Password, salt, ownersRestaurant);

            await _ownerRepository.AddAsync(ownerToRegister);
        }

        public async Task UpdateAsync(OwnerOnUpdateDTO ownerDto)
        {
            var owner = await _ownerRepository.GetAsync(ownerDto.Login);

            if (owner == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "Owner doesn't exist.");

            owner.Login = ownerDto.Login;
            owner.FirstName = ownerDto.FirstName;
            owner.LastName = ownerDto.LastName;
            owner.Email = ownerDto.Email;
            owner.Phone = ownerDto.Phone;

            await _ownerRepository.UpdateAsync(owner);
        }
    }
}
