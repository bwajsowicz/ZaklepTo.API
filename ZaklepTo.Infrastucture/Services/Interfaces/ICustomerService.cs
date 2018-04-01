using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;

namespace ZaklepTo.Infrastructure.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDTO> GetAsync(string login);
        Task<IEnumerable<CustomerDTO>> GetAllAsync();
        Task LoginAsync(string login, string password);
        Task RegisterAsync(CustomerOnCreateDTO customer);
        Task UpdateAsync(CustomerOnUpdateDTO customerDto);
        Task ChangePassword(PasswordChange passwordChange);
        Task DeleteAsync(string login);
        Task<IEnumerable<RestaurantDTO>> GetMostFrequentRestaurants(string login);
    }
}