using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;
        private readonly ISettingService settingService;

        public FooterViewComponent(ICategoryService categoryService,ISettingService settingService)
        {
            this.categoryService = categoryService;
            this.settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var popcategories = await categoryService.GetPopularCategoryAsync();

            var setting = await settingService.GetSettings();

            var logo = setting.FooterLogo;
            var desc = setting.FooterDescription;
        
 
            var socialmedias = new Dictionary<string, string>();
            socialmedias.Add("instagram",setting.InstagramUrl);
            socialmedias.Add("twitter", setting.Twitterurl);
            socialmedias.Add("youtube",setting.Youtubeurl);
            socialmedias.Add("facebook",setting.FacebookUrl);


            return View((popcategories,desc,socialmedias,logo));
        }

    }
}
