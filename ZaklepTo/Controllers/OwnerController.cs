using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.Services.Interfaces;

namespace ZaklepTo.API.Controllers
{
    [Route("api/owners")]
    public class OwnerController : Controller
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetSingleOwner(string login)
        {
            var owner = await _ownerService.GetAsync(login);

            return Ok(owner);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllOwners()
        {
            var owners = await _ownerService.GetAllAsync();

            return Ok(owners);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewOwner([FromBody] OwnerOnCreateDTO ownerToRegister)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _ownerService.RegisterAsync(ownerToRegister);

            return Created($"{ownerToRegister.Login}", Json(ownerToRegister));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginOwner([FromBody] LoginCredentials loginCredentials)
        {
            await _ownerService.LoginAsync(loginCredentials);

            return Ok();
        }

        [HttpPost("{login}/update")]
        public async Task<IActionResult> UpdateOwner([FromBody] OwnerOnUpdateDTO updatedOwner)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _ownerService.UpdateAsync(updatedOwner);

            return Ok();
        }

        [HttpPost("{login}/changepassword")]
        public async Task<IActionResult> ChangeOwnersPassword([FromBody] PasswordChange passwordChange)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _ownerService.ChangePassword(passwordChange);

            return Ok();
        }

        [HttpDelete("{login}/remove")]
        public async Task<IActionResult> RemoveOwner(string login)
        {
            await _ownerService.DeleteAsync(login);

            return Ok();
        }
    }
}