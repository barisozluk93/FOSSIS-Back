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
    public class HeatPumpController : ControllerBase
    {
        private readonly IHeatPumpService _heatPumpService;

        public HeatPumpController(IHeatPumpService heatPumpService)
        {
            _heatPumpService = heatPumpService;
        }

        [HttpGet("Paginate")]
        [Authorize]
        [HasPermission("HeatPumpScene.Paging.Permission")]
        public async Task<IActionResult> Paginate([FromQuery] PagingParameter pagingParameter)
        {
            var result = await _heatPumpService.Paginate(pagingParameter);
            return new OkObjectResult(result);
        }

        [HttpGet("All")]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            var result = await _heatPumpService.GetHeatPumps();
            return new OkObjectResult(result);
        }

        [HttpPost("Save")]
        [Authorize]
        [HasPermission("HeatPumpScene.Save.Permission")]

        public async Task<IActionResult> Save([FromBody] HeatPump heatPump)
        {
            var result = await _heatPumpService.Save(heatPump);
            return new OkObjectResult(result);
        }

        [HttpPost("Update")]
        [Authorize]
        [HasPermission("HeatPumpScene.Edit.Permission")]

        public async Task<IActionResult> Update([FromBody] HeatPump heatPump)
        {
            var result = await _heatPumpService.Update(heatPump);
            return new OkObjectResult(result);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        [HasPermission("HeatPumpScene.Delete.Permission")]

        public async Task<IActionResult> Delete(long id)
        {
            var result = await _heatPumpService.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(long id)
        {
            var result = await _heatPumpService.GetById(id);
            return new OkObjectResult(result);
        }
    }
}
