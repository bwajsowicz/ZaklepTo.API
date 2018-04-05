using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;

namespace ZaklepTo.Infrastructure.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<OwnerDTO> GetAsync(string login);
        Task<IEnumerable<OwnerDTO>> GetAllAsync();
        Task LoginAsync(LoginCredentials loginCredentials);
        Task RegisterAsync(OwnerOnCreateDTO ownerDto);
        Task UpdateAsync(OwnerOnUpdateDTO ownerDto);
        Task ChangePassword(PasswordChange passwordChange);
        Task DeleteAsync(string login);
    }
}