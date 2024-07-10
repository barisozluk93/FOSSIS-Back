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
    public class InverterController : ControllerBase
    {
        private readonly IInverterService _inverterService;

        public InverterController(IInverterService inverterService)
        {
            _inverterService = inverterService;
        }

        [HttpGet("Paginate")]
        [Authorize]
        [HasPermission("InverterScene.Paging.Permission")]
        public async Task<IActionResult> Paginate([FromQuery] PagingParameter pagingParameter)
        {
            var result = await _inverterService.Paginate(pagingParameter);
            return new OkObjectResult(result);
        }

        [HttpGet("All")]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            var result = await _inverterService.GetInverters();
            return new OkObjectResult(result);
        }

        [HttpPost("Save")]
        [Authorize]
        [HasPermission("InverterScene.Save.Permission")]

        public async Task<IActionResult> Save([FromBody] Inverter inverter)
        {
            var result = await _inverterService.Save(inverter);
            return new OkObjectResult(result);
        }

        [HttpPost("Update")]
        [Authorize]
        [HasPermission("InverterScene.Edit.Permission")]

        public async Task<IActionResult> Update([FromBody] Inverter inverter)
        {
            var result = await _inverterService.Update(inverter);
            return new OkObjectResult(result);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        [HasPermission("InverterScene.Delete.Permission")]

        public async Task<IActionResult> Delete(long id)
        {
            var result = await _inverterService.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(long id)
        {
            var result = await _inverterService.GetById(id);
            return new OkObjectResult(result);
        }
    }
}
