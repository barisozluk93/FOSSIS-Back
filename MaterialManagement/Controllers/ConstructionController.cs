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
    public class ConstructionController : ControllerBase
    {
        private readonly IConstructionService _constructionService;

        public ConstructionController(IConstructionService constructionService)
        {
            _constructionService = constructionService;
        }

        [HttpGet("Paginate")]
        [Authorize]
        [HasPermission("ConstructionScene.Paging.Permission")]
        public async Task<IActionResult> Paginate([FromQuery] PagingParameter pagingParameter)
        {
            var result = await _constructionService.Paginate(pagingParameter);
            return new OkObjectResult(result);
        }

        [HttpGet("All")]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            var result = await _constructionService.GetConstructions();
            return new OkObjectResult(result);
        }

        [HttpPost("Save")]
        [Authorize]
        [HasPermission("ConstructionScene.Save.Permission")]

        public async Task<IActionResult> Save([FromBody] Construction construction)
        {
            var result = await _constructionService.Save(construction);
            return new OkObjectResult(result);
        }

        [HttpPost("Update")]
        [Authorize]
        [HasPermission("ConstructionScene.Edit.Permission")]

        public async Task<IActionResult> Update([FromBody] Construction construction)
        {
            var result = await _constructionService.Update(construction);
            return new OkObjectResult(result);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        [HasPermission("ConstructionScene.Delete.Permission")]

        public async Task<IActionResult> Delete(long id)
        {
            var result = await _constructionService.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(long id)
        {
            var result = await _constructionService.GetById(id);
            return new OkObjectResult(result);
        }
    }
}
