using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastucture.DTO;
using ZaklepTo.Infrastucture.DTO.OnCreate;
using ZaklepTo.Infrastucture.DTO.OnUpdate;

namespace ZaklepTo.Infrastucture.Services.Interfaces
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