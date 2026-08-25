using Blog.Entity.DTOs.SiteSettings;
using Blog.Service.Services.Abstractions;
using Blog.Web.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Policy = "SettingsViewPolicy")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settings = await _settingService.GetSettings();
            return Ok(settings);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] SiteSettingsUpdateModel settingsUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var newSettings = await _settingService.UpdateSettings(settingsUpdateDto);
                if (newSettings != null)
                {
                    return Ok(new { success = true, message = "Ayarlar başarıyla güncellendi." });
                }
            }
            return BadRequest(new { success = false, message = "Ayarlar güncellenemedi." });
        }

        [HttpGet("pages/{pageTitleRaw}")]
        public async Task<IActionResult> GetPage(string pageTitleRaw)
        {
            var page = await _settingService.GetPageAsync(pageTitleRaw);
            if (page == null)
                return NotFound(new { success = false, message = "Sayfa bulunamadı." });
            
            return Ok(page);
        }

        [HttpPut("pages")]
        public async Task<IActionResult> UpdatePage([FromBody] Blog.Entity.DTOs.Page.PageUpdateModel pageUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var updatedPage = await _settingService.UpdatePageAsync(pageUpdateDto);
                if (updatedPage != null)
                {
                    return Ok(new { success = true, message = "Sayfa başarıyla güncellendi." });
                }
            }
            return BadRequest(new { success = false, message = "Sayfa güncellenemedi." });
        }
    }
}
