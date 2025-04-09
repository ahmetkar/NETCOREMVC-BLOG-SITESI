using Blog.Entity.Entities;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MediaController : Controller
    {
        private readonly IImageService imageService;
   

        public MediaController(IImageService imageService)
        {
            this.imageService = imageService;
            

        }
        public async  Task<IActionResult> Index()
        {

            var images =  await imageService.GetAllImagesAsync();

            return View(images);
        }

        [HttpGet]
        public async  Task<IActionResult> DeletedMedias()
        {
            var images = await imageService.GetAllDeletedImagesAsync();
            return View(images);
        }

        [HttpGet]
        public async Task<IActionResult> SafeDelete(Guid? imageId)
        {
            if (imageId != null)
            {
                await imageService.SafeDeleteImageAsync(imageId.Value);
                await imageService.SaveAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ForceDelete(Guid? imageId)
        {
            if (imageId != null)
            {
                await imageService.ForceDeleteImageAsync(null, imageId.Value);
                await imageService.SaveAsync();
            }
            return RedirectToAction("DeletedMedias");
        }


    }
}
