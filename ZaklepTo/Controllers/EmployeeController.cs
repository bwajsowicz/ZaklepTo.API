using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.Services.Interfaces;

namespace ZaklepTo.API.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetSingleEmployee(string login)
        {
            var employee = await _employeeService.GetAsync(login);

            return Ok(employee);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllAsync();

            return Ok(employees);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewEmployee([FromBody] EmployeeOnCreateDTO employeeToRegister)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _employeeService.RegisterAsync(employeeToRegister);

            return Created($"{employeeToRegister.Login}", Json(employeeToRegister));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginEmployee([FromBody] LoginCredentials loginCredentials)
        {
            await _employeeService.LoginAsync(loginCredentials);

            return Ok();
        }

        [HttpPost("{login}/update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeOnUpdateDTO updatedEmployee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _employeeService.UpdateAsync(updatedEmployee);

            return Ok();
        }

        [HttpPost("{login}/changepassword")]
        public async Task<IActionResult> ChangeEmployeesPassword([FromBody] PasswordChange passwordChange)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _employeeService.ChangePassword(passwordChange);

            return Ok();
        }

        [HttpDelete("{login}/remove")]
        public async Task<IActionResult> RemoveEmployee(string login)
        {
            await _employeeService.DeleteAsync(login);

            return Ok();
        }
    }
}