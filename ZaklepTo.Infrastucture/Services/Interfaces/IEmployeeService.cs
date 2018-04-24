using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;

namespace ZaklepTo.Infrastructure.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetAsync(string login);
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task LoginAsync(LoginCredentials loginCredentials);
        Task RegisterAsync(EmployeeOnCreateDto employeeDto);
        Task UpdateAsync(EmployeeOnUpdateDto employeeDto);
        Task ChangePassword(PasswordChange passwordChange);
        Task DeleteAsync(string login);
    }
}