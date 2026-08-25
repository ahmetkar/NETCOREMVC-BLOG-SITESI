using Blog.Service.Services.Abstractions;
using Blog.Web.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
    public class MediaController : ControllerBase
    {
        private readonly IImageService _imageService;

        public MediaController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var images = await _imageService.GetAllImagesAsync();
            return Ok(images);
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> DeletedMedias()
        {
            var images = await _imageService.GetAllDeletedImagesAsync();
            return Ok(images);
        }

        [HttpDelete("safe/{id}")]
        public async Task<IActionResult> SafeDelete(Guid id)
        {
            await _imageService.SafeDeleteImageAsync(id);
            await _imageService.SaveAsync();
            return Ok(new { success = true, message = "Resim başarıyla çöp kutusuna taşındı." });
        }

        [HttpDelete("force/{id}")]
        public async Task<IActionResult> ForceDelete(Guid id)
        {
            await _imageService.ForceDeleteImageAsync(null, id);
            await _imageService.SaveAsync();
            return Ok(new { success = true, message = "Resim kalıcı olarak silindi." });
        }
    }
}
