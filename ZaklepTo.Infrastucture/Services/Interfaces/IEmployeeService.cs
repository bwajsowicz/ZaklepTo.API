using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;

namespace ZaklepTo.Infrastructure.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetAsync(string login);
        Task<IEnumerable<EmployeeDTO>> GetAllAsync();
        Task LoginAsync(PasswordChange passwordChange);
        Task RegisterAsync(EmployeeOnCreateDTO employeeDto);
        Task UpdateAsync(EmployeeOnUpdateDTO employeeDto);
        Task ChangePassword(PasswordChange passwordChange);
        Task DeleteAsync(string login);
    }
}