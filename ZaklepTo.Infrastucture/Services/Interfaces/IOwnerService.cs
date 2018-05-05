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
        Task<OwnerDto> GetAsync(string login);
        Task<IEnumerable<OwnerDto>> GetAllAsync();
        Task LoginAsync(LoginCredentials loginCredentials);
        Task RegisterAsync(OwnerOnCreateDto ownerDto);
        Task UpdateAsync(OwnerOnUpdateDto ownerDto, string login);
        Task ChangePassword(PasswordChange passwordChange, string login);
        Task DeleteAsync(string ownersLogin);
    }
}