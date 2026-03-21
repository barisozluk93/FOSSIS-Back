using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MapManagementService.Controllers
{

    [Route("api2/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        public MapController()
        {
        }

        [HttpGet("GetBuildings")]
        public async Task<JsonResult> GetBuildings()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Buildings\\beyoglu.json");
            using StreamReader reader = new(path);
            return new JsonResult(reader.ReadToEnd());
        }
    }
}