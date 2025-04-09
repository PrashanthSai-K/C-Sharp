using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement_RESTAPI.Services.Contracts;
using TaskManagement_RESTAPI.Shared.DTO;

namespace TaskManagement_RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _serviceManager.UserService.GetAllUsers();
            return Ok(new { Data = users });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(CreateUser user)
        {
            await _serviceManager.UserService.CreateUser(user);
            return Ok(new { Message = "User created successfully" });
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(UpdateUser user)
        {
            Console.WriteLine("sdfsdf  " + user.Id);
            await _serviceManager.UserService.UpdateUser(user);
            return Ok(new { Data = "User updated successfully" });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _serviceManager.UserService.DeleteUser(id);
            return Ok(new { Message = "User deleted successfully." });
        }
    }
}
