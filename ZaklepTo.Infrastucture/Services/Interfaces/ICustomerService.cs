using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;

namespace ZaklepTo.Infrastructure.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetAsync(string login);
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task LoginAsync(LoginCredentials loginCredentials);
        Task RegisterAsync(CustomerOnCreateDto customer);
        Task UpdateAsync(CustomerOnUpdateDto customerDto);
        Task ChangePassword(PasswordChange passwordChange);
        Task DeleteAsync(string login);
        Task<IEnumerable<RestaurantDto>> GetMostFrequentRestaurants(string login);
    }
}