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
    public class ChargingStationController : ControllerBase
    {
        private readonly IChargingStationService _chargingStationService;

        public ChargingStationController(IChargingStationService chargingStationService)
        {
            _chargingStationService = chargingStationService;
        }

        [HttpGet("Paginate")]
        [Authorize]
        [HasPermission("ChargingStationScene.Paging.Permission")]
        public async Task<IActionResult> Paginate([FromQuery] PagingParameter pagingParameter)
        {
            var result = await _chargingStationService.Paginate(pagingParameter);
            return new OkObjectResult(result);
        }

        [HttpGet("All")]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            var result = await _chargingStationService.GetChargingStations();
            return new OkObjectResult(result);
        }

        [HttpPost("Save")]
        [Authorize]
        [HasPermission("ChargingStationScene.Save.Permission")]

        public async Task<IActionResult> Save([FromBody] ChargingStation chargingStation)
        {
            var result = await _chargingStationService.Save(chargingStation);
            return new OkObjectResult(result);
        }

        [HttpPost("Update")]
        [Authorize]
        [HasPermission("ChargingStationScene.Edit.Permission")]

        public async Task<IActionResult> Update([FromBody] ChargingStation chargingStation)
        {
            var result = await _chargingStationService.Update(chargingStation);
            return new OkObjectResult(result);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        [HasPermission("ChargingStationScene.Delete.Permission")]

        public async Task<IActionResult> Delete(long id)
        {
            var result = await _chargingStationService.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(long id)
        {
            var result = await _chargingStationService.GetById(id);
            return new OkObjectResult(result);
        }
    }
}
