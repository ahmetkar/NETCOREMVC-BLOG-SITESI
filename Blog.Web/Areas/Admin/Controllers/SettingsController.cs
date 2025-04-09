using AutoMapper;
using Blog.Entity.DTOs.Page;
using Blog.Entity.DTOs.SiteSettings;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using Blog.Web.Consts;
using Blog.Web.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly ISettingService settingService;
        private readonly IToastNotification toast;
        private readonly IMapper mapper;

        public SettingsController(ISettingService settingService, IToastNotification toast,IMapper mapper)
        {
            this.settingService = settingService;
            this.toast = toast;
            this.mapper = mapper;
        }

        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async  Task<IActionResult> Index()
        {

            var settings = await settingService.GetSettings();
            var settingsforview = mapper.Map<SiteSettingsUpdateModel>(settings);

            return View(settingsforview);
        }



        [HttpPost]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Index(SiteSettingsUpdateModel settings)
        {

            var newSettings = await settingService.UpdateSettings(settings);

            if (!newSettings.Equals(null))
            {
                toast.AddSuccessToastMessage("Ayarlar başarıyla güncellendi..", new ToastrOptions { Title = "Başarılı" });
            }
            else
            {
                toast.AddErrorToastMessage("Ayarlar eklenemedi..", new ToastrOptions { Title = "Başarısız" });
            }

            var oldsettings = await settingService.GetSettings();
            var settingsforview = mapper.Map<SiteSettingsUpdateModel>(oldsettings);

            return View(settingsforview);

        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> UpdatePages(string whichPage)
        {

            var getPage = await settingService.GetPageAsync(whichPage);
            if (getPage != null)
            {
                var map = mapper.Map<PageUpdateModel>(getPage);
                map.Id = getPage.Id;
                return View(map);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> UpdatePages(PageUpdateModel page)
        {
            
            var updatePage = await settingService.UpdatePageAsync(page);

            if(updatePage != null)
            {
                toast.AddSuccessToastMessage("Sayfa başarıyla güncellendi..", new ToastrOptions { Title = "Başarılı" });
               
            }
            else
            {
                toast.AddErrorToastMessage("Sayfa güncellenemedi..", new ToastrOptions { Title = "Başarısız" });
               
            }

            return View(page);

        }

      

    }


}
