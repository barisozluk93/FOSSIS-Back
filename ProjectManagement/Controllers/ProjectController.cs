using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ProjectManagement.Authorization;
using ProjectManagement.Entity;
using ProjectManagement.Model;

namespace ProjectManagement.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("Paginate/{userId}")]
        [Authorize]
        public async Task<IActionResult> Paginate([FromQuery] PagingParameter pagingParameter, long userId)
        {
            var result = await _projectService.Paginate(pagingParameter, userId);
            return new OkObjectResult(result);
        }

        [HttpGet("All")]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            var result = await _projectService.GetProjects();
            return new OkObjectResult(result);
        }

        [HttpGet("Pvcalc")]
        [Authorize]
        public async Task<IActionResult> GetPvCalc([FromQuery] PvcCalcMonthlyParam pvcCalcMonthlyParam)
        {
            var result = await _projectService.GetPvCalcMonthly(pvcCalcMonthlyParam);
            return new OkObjectResult(result);
        }

        [HttpGet("Seriescalc")]
        [Authorize]
        public async Task<IActionResult> GetPvCalc([FromQuery] SeriesCalcDailyParam seriesCalcDailyParam)
        {
            var result = await _projectService.GetSeriesCalcDaily(seriesCalcDailyParam);
            return new OkObjectResult(result);
        }

        [HttpPost("Save")]
        [Authorize]

        public async Task<IActionResult> Save([FromBody] Project project)
        {
            var result = await _projectService.Save(project);
            return new OkObjectResult(result);
        }

        [HttpPost("Update")]
        [Authorize]

        public async Task<IActionResult> Update([FromBody] Project project)
        {
            var result = await _projectService.Update(project);
            return new OkObjectResult(result);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]

        public async Task<IActionResult> Delete(long id)
        {
            var result = await _projectService.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(long id)
        {
            var result = await _projectService.GetById(id);
            return new OkObjectResult(result);
        }

    }
}
