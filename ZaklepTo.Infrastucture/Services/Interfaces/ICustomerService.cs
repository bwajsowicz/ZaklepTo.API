using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastucture.DTO;

namespace ZaklepTo.Infrastucture.Services.Implementations
{
    public interface ICustomerService
    {
        Task<CustomerDTO> GetAsync(string login);
        Task<IEnumerable<CustomerDTO>> GetAllAsync();
        Task LoginAsync(string login, string password);

        Task RegisterAsync(string login, string firstname, string lastname,
            string email, string phone, string password);

        Task UpdateAsync(CustomerDTO customerDto);
        Task DeleteAsync(string login);
        Task<IEnumerable<RestaurantDTO>> GetMostFrequentRestaurations(string login);
    }
}