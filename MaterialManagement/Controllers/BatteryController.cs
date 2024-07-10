using MaterialManagement.Authorization;
using MaterialManagement.Entity;
using MaterialManagement.Interfaces;
using MaterialManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MaterialManagement.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class BatteryController : ControllerBase
    {
        private readonly IBatteryService _batteryService;

        public BatteryController(IBatteryService batteryService)
        {
            _batteryService = batteryService;
        }

        [HttpGet("Paginate")]
        [Authorize]
        [HasPermission("BatteryScene.Paging.Permission")]
        public async Task<IActionResult> Paginate([FromQuery] PagingParameter pagingParameter)
        {
            var result = await _batteryService.Paginate(pagingParameter);
            return new OkObjectResult(result);
        }

        [HttpGet("All")]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            var result = await _batteryService.GetBatteries();
            return new OkObjectResult(result);
        }

        [HttpPost("Save")]
        [Authorize]
        [HasPermission("BatteryScene.Save.Permission")]

        public async Task<IActionResult> Save([FromBody] Battery battery)
        {
            var result = await _batteryService.Save(battery);
            return new OkObjectResult(result);
        }

        [HttpPost("Update")]
        [Authorize]
        [HasPermission("BatteryScene.Edit.Permission")]

        public async Task<IActionResult> Update([FromBody] Battery battery)
        {
            var result = await _batteryService.Update(battery);
            return new OkObjectResult(result);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        [HasPermission("BatteryScene.Delete.Permission")]

        public async Task<IActionResult> Delete(long id)
        {
            var result = await _batteryService.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(long id)
        {
            var result = await _batteryService.GetById(id);
            return new OkObjectResult(result);
        }
    }
}
