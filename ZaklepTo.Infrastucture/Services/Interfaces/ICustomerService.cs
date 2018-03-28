using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastucture.DTO;

namespace ZaklepTo.Infrastucture.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDTO> GetAsync(string login);
        Task<IEnumerable<CustomerDTO>> GetAllAsync();
        Task LoginAsync(string login, string password);

        Task RegisterAsync(string login, string firstname, string lastname,
            string email, string phone, string password);

        Task UpdateAsync(CustomerDTO customerDto);
        Task ChangePassword(string login, string oldPassword, string newPassword);
        Task DeleteAsync(string login);
        Task<IEnumerable<RestaurantDTO>> GetMostFrequentRestaurants(string login);
    }
}