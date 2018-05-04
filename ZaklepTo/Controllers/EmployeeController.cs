using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IJwtService _jwtHandler;

        public EmployeeController(IEmployeeService employeeService, IJwtService jwtHandler)
        {
            _employeeService = employeeService;
            _jwtHandler = jwtHandler;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllAsync();

            return Ok(employees);
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetSingleEmployee(string login)
        {
            var employee = await _employeeService.GetAsync(login);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewEmployee([FromBody] EmployeeOnCreateDto employeeToRegister)
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

            var token = _jwtHandler.CreateToken(loginCredentials.Login, "employee");

            return Ok();
        }

        [HttpPost("{login}/update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeOnUpdateDto updatedEmployee)
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