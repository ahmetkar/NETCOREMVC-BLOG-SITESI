using Blog.Service.Services.Abstractions;
using Blog.Entity.DTOs.Subscriber;
using Blog.Web.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor}")]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;

        public SubscribersController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var subscribers = await _subscriberService.GetSubscribersAsync();
            return Ok(subscribers);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _subscriberService.DeleteSubscriberAsync(id, User.Identity?.Name);
            if (!deleted)
            {
                return NotFound(new { success = false, message = "Abone bulunamadı." });
            }

            return Ok(new { success = true, message = "Abone listeden kaldırıldı." });
        }

        [HttpPut("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] SubscriberStatusUpdateModel request)
        {
            var updated = await _subscriberService.SetSubscriberActiveAsync(id, request.IsActive, User.Identity?.Name);
            if (!updated)
            {
                return NotFound(new { success = false, message = "Abone bulunamadı." });
            }

            return Ok(new { success = true, message = request.IsActive ? "Abone bülten gönderimine açıldı." : "Abone bülten gönderimine kapatıldı." });
        }
    }
}
