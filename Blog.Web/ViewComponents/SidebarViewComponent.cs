using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly ISettingService settingService;

        public SidebarViewComponent(IArticleService articleService,ISettingService settingService)
        {
            this.articleService = articleService;
            this.settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = await settingService.GetSettings();


            var topitem = setting.SidebarTopArticleSetting == "last-added" ? await articleService.GetLastFiveArticlesAsync() :
                setting.SidebarTopArticleSetting == "most-commented" ? await articleService.GetMostCommentedArticlesAsync() :
               await articleService.GetMostVisitedArticlesAsync();

            var bottomitem = setting.SidebarBottomArticleSetting == "last-added" ? await articleService.GetLastFiveArticlesAsync() :
               setting.SidebarBottomArticleSetting == "most-viewed" ? await articleService.GetMostVisitedArticlesAsync() :
               await articleService.GetMostCommentedArticlesAsync();

      
           
            var articlesettings  =new Dictionary<string,string>{};
            articlesettings.Add("top", setting.SidebarTopArticleSetting);
            articlesettings.Add("bottom", setting.SidebarBottomArticleSetting);


            var socialmedias = new Dictionary<string,string> { };
       
            socialmedias.Add("instagram", setting.InstagramUrl);
            socialmedias.Add("twitter", setting.Twitterurl);
            socialmedias.Add("youtube", setting.Youtubeurl);
            socialmedias.Add("facebook", setting.FacebookUrl);

            return View((topitem,bottomitem,articlesettings,socialmedias));
        }
    }

}
