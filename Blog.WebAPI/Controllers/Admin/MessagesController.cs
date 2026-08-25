using Blog.Service.Services.Abstractions;
using Blog.Web.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor}")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService _messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var messages = await _messagesService.GetMessagesAsync();
            return Ok(messages);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var title = await _messagesService.DeleteMessageAsync(id);
            return Ok(new { success = true, message = $"Mesaj başarıyla silindi." });
        }
    }
}
