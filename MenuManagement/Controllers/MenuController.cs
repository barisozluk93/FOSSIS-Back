using MenuManagement.Authorization;
using MenuManagement.Entity;
using MenuManagement.Interfaces;
using MenuManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("Paginate")]
        [Authorize]
        [HasPermission("MenuScene.List.Permission")]
        public async Task<IActionResult> Paginate([FromQuery] PagingParameter pagingParameter)
        {
            var result = await _menuService.Paginate(pagingParameter);
            return new OkObjectResult(result);
        }

        [HttpGet("All")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _menuService.GetMenus();
            return new OkObjectResult(result);
        }

        [HttpGet("GetMenuList")]
        [Authorize]
        public async Task<IActionResult> GetMenuList()
        {
            var result = await _menuService.GetMenuList();
            return new OkObjectResult(result);
        }

        [HttpPost("Save")]
        [Authorize]
        [HasPermission("MenuScene.Save.Permission")]
        public async Task<IActionResult> Save([FromBody] Menu menu)
        {
            var result = await _menuService.Save(menu);
            return new OkObjectResult(result);
        }

        [HttpPost("Update")]
        [HasPermission("MenuScene.Edit.Permission")]
        [Authorize]

        public async Task<IActionResult> Update([FromBody] Menu menu)
        {
            var result = await _menuService.Update(menu);
            return new OkObjectResult(result);
        }

        [HttpDelete("Delete/{id}")]
        [HasPermission("MenuScene.Delete.Permission")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _menuService.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(long id)
        {
            var result = await _menuService.GetById(id);
            return new OkObjectResult(result);
        }

    }
}
