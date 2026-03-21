using MaterialManagement.Authorization;
using MaterialManagement.Entity;
using MaterialManagement.Interfaces;
using MaterialManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MaterialManagement.Controllers
{
    [Route("/api2/[controller]")]
    [ApiController]
    public class CableController : ControllerBase
    {
        private readonly ICableService _cableService;

        public CableController(ICableService cableService)
        {
            _cableService = cableService;
        }

        [HttpGet("Paginate")]
        [Authorize]
        [HasPermission("CableScene.Paging.Permission")]
        public async Task<IActionResult> Paginate([FromQuery] PagingParameter pagingParameter)
        {
            var result = await _cableService.Paginate(pagingParameter);
            return new OkObjectResult(result);
        }

        [HttpGet("All")]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            var result = await _cableService.GetCables();
            return new OkObjectResult(result);
        }

        [HttpPost("Save")]
        [Authorize]
        [HasPermission("CableScene.Save.Permission")]

        public async Task<IActionResult> Save([FromBody] Cable cable)
        {
            var result = await _cableService.Save(cable);
            return new OkObjectResult(result);
        }

        [HttpPost("Update")]
        [Authorize]
        [HasPermission("CableScene.Edit.Permission")]

        public async Task<IActionResult> Update([FromBody] Cable cable)
        {
            var result = await _cableService.Update(cable);
            return new OkObjectResult(result);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        [HasPermission("CableScene.Delete.Permission")]

        public async Task<IActionResult> Delete(long id)
        {
            var result = await _cableService.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(long id)
        {
            var result = await _cableService.GetById(id);
            return new OkObjectResult(result);
        }
    }
}
