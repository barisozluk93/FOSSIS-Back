using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using NotificationManagement.Entity;
using NotificationManagement.Interfaces;
using NotificationManagement.Model;
using Microsoft.AspNetCore.Authorization;
using NotificationManagement.Authorization;

namespace NotificationManagement.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("All")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _notificationService.GetNotifications();
            return new OkObjectResult(result);
        }

        [HttpPost("Save")]
        [Authorize]
        public async Task<IActionResult> Save([FromBody] Notification notification)
        {
            var result = await _notificationService.Save(notification);
            return new OkObjectResult(result);
        }

        [HttpPost("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] Notification notification)
        {
            var result = await _notificationService.Update(notification);
            return new OkObjectResult(result);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _notificationService.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpGet("Read/{id}")]
        [Authorize]

        public async Task<IActionResult> Read(long id)
        {
            var result = await _notificationService.Read(id);
            return new OkObjectResult(result);
        }
    }
}
