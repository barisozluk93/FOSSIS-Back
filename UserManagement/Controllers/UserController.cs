using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using UserManagement.Authorization;
using UserManagement.Entity;
using UserManagement.Interfaces;
using UserManagement.Model;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Paginate")]
        [Authorize]
        [HasPermission("UserScene.Paging.Permission")]

        public async Task<IActionResult> Paginate([FromQuery] PagingParameter pagingParameter)
        {
            var result = await _userService.Paginate(pagingParameter);
            return new OkObjectResult(result);
        }

        [HttpGet("All")]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetUsers();
            return new OkObjectResult(result);
        }

        [HttpPost("Save")]
        [Authorize]
        [HasPermission("UserScene.Save.Permission")]

        public async Task<IActionResult> Save([FromBody] User user)
        {
            var result = await _userService.Save(user);
            return new OkObjectResult(result);
        }

        [HttpPost("Update")]
        [Authorize]
        [HasPermission("UserScene.Edit.Permission")]

        public async Task<IActionResult> Update([FromBody] User user)
        {
            var result = await _userService.Update(user);
            return new OkObjectResult(result);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        [HasPermission("UserScene.Delete.Permission")]

        public async Task<IActionResult> Delete(long id)
        {
            var result = await _userService.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(long id)
        {
            var result = await _userService.GetById(id);
            return new OkObjectResult(result);
        }

        [HttpGet("GetUserPermissions")]
        [Authorize]

        public async Task<IActionResult> GetUserPermissions()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();

            var result = await _userService.GetUserPermissions(token);
            return new OkObjectResult(result);
        }
    }
}
