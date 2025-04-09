using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessagesController : Controller
    {
        private readonly IMessagesService msgService;

        public MessagesController(IMessagesService msgService) {
            this.msgService = msgService;
        }

        public async Task<IActionResult> Index()

        {
            var messages = await msgService.GetMessagesAsync();

            return View(messages);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(Guid msgId)
        {
            var delete = await msgService.DeleteMessageAsync(msgId);


            return RedirectToAction("Index");
        }
    }
}
