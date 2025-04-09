using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
    public class LogoViewComponent : ViewComponent
    {
        private readonly ISettingService settingService;

        public LogoViewComponent(ISettingService settingService)
        {
            this.settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = await settingService.GetSettings();
            var title = setting.SiteTitle != null ? setting.SiteTitle : "Blog We";
            var logo = setting.LogoImage.FileName != null ? setting.LogoImage.FileName : "images/site-images/tech-logo.png";
            return View((title,logo));
        }
    }
}
