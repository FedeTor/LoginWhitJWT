using Application.Dtos;
using Application.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller 
    {
        private readonly IServiceCreateUser _serviceCreateUser;
        private readonly IServiceUpdateUser _serviceUpdateUser;
        private readonly IServiceGetUser _serviceGetUser;
        private readonly IServiceDeleteUser _serviceDeleteUser;
        private readonly IServiceGetAllUsers _serviceGetAllUsers;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IServiceCreateUser serviceCreateUser,
            IServiceUpdateUser serviceUpdateUser,
            IServiceGetUser serviceGetUser,
            IServiceDeleteUser serviceDeleteUser,
            IServiceGetAllUsers serviceGetAllUsers,
            ILogger<UserController> logger)
        {
            _serviceCreateUser = serviceCreateUser;
            _serviceUpdateUser = serviceUpdateUser;
            _serviceGetUser = serviceGetUser;
            _serviceDeleteUser = serviceDeleteUser;
            _serviceGetAllUsers = serviceGetAllUsers;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(UserDtoIn userDto)
        {
            _logger.LogInformation("Creating user with details: {0}", JsonConvert.SerializeObject(userDto));
            await _serviceCreateUser.CreateUserAsync(userDto);

            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDtoIn userDto)
        {
            _logger.LogInformation("Updating user with details: {0}", JsonConvert.SerializeObject(userDto));
            await _serviceUpdateUser.UpdateUserAsync(userDto);

            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetUser(int id)
        {
            _logger.LogInformation("Fetching user with ID: {UserId}", id);
            var user = await _serviceGetUser.GetUserAsync(id);

            return Ok(user);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(bool onlyActive = false)
        {
            _logger.LogInformation("Fetching all users with onlyActive={OnlyActive}", onlyActive);
            var users = await _serviceGetAllUsers.GetAllUsersAsync(onlyActive);

            return Ok(users);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("Deleting users with IDs: {@UserIds}", id);
            await _serviceDeleteUser.DeleteUserAsync(id);

            return Ok();
        }
    }
}
