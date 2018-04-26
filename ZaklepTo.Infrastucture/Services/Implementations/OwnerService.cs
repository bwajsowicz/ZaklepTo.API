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
            if (owner == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "OwnerEntity doesn't exist.");

            var oldPasswordHash = _encrypter.GetHash(passwordChange.OldPassword, owner.Salt);

            if (owner.Password != oldPasswordHash)
                throw new ServiceException(ErrorCodes.InvalidPassword, "Invalid password.");

            var salt = _encrypter.GetSalt(passwordChange.NewPassword);
            var hash = _encrypter.GetHash(passwordChange.NewPassword, salt);

            owner.Password = hash;

            await _ownerRepository.UpdateAsync(owner);
        }

        public async Task DeleteAsync(string ownersLogin)
        {
            var ownerToDelete = await _ownerRepository.GetAsync(ownersLogin);
            if (ownerToDelete == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "OwnerEntity doesn't exist.");

            await _ownerRepository.DeleteAsync(ownersLogin);
        }

        public async Task<IEnumerable<OwnerDto>> GetAllAsync()
        {
            var owners = await _ownerRepository.GetAllAsync();
            return owners.Select(owner => _mapper.Map<Owner, OwnerDto>(owner));
        }

        public async Task<OwnerDto> GetAsync(string login)
        {
            var ownerToGet = await _ownerRepository.GetAsync(login);
            if(ownerToGet == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "OwnerEntity doesn't exist.");

            return _mapper.Map<Owner, OwnerDto>(ownerToGet);
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

        public async Task RegisterAsync(OwnerOnCreateDto ownerDto)
        {
            var owner = await _ownerRepository.GetAsync(ownerDto.Login);
            if (owner != null)
                throw new ServiceException(ErrorCodes.OwnerAlreadyExists, "Login is already in use.");

            var salt = _encrypter.GetSalt(ownerDto.Password);
            var hash = _encrypter.GetHash(ownerDto.Password, salt);
            
            var ownersRestaurant = await _restaurantRepository.GetAsync(ownerDto.Restaurant.Id);

            var ownerToRegister = new Owner(ownerDto.Login, ownerDto.FirstName, ownerDto.LastName, ownerDto.Email, 
                ownerDto.Phone, ownerDto.Password, salt, ownersRestaurant);

            await _ownerRepository.AddAsync(ownerToRegister);
        }

        public async Task UpdateAsync(OwnerOnUpdateDto ownerDto)
        {
            var ownerToUpdate = await _ownerRepository.GetAsync(ownerDto.Login);
            if (ownerToUpdate == null)
                throw new ServiceException(ErrorCodes.OwnerNotFound, "OwnerEntity doesn't exist.");

            ownerToUpdate.Login = ownerDto.Login;
            ownerToUpdate.FirstName = ownerDto.FirstName;
            ownerToUpdate.LastName = ownerDto.LastName;
            ownerToUpdate.Email = ownerDto.Email;
            ownerToUpdate.Phone = ownerDto.Phone;

            await _ownerRepository.UpdateAsync(ownerToUpdate);
        }
    }
}
