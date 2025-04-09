using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
    public class TopViewComponent : ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly ISettingService settingService;

        public TopViewComponent(IArticleService articleService,ISettingService settingService)
        {
            this.articleService = articleService;
            this.settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = await settingService.GetSettings();
            var topsetting = setting.TopArticleSetting;

            var mcArticles = await articleService.GetMostCommentedArticlesAsync();
            var mvArticles = await articleService.GetMostVisitedArticlesAsync();
            var laArticles = await articleService.GetLastFiveArticlesAsync();
                
            var item = topsetting == "most-commented" ? mcArticles.Take(3).ToList():
               topsetting == "most-viewed" ?  mvArticles.Take(3).ToList():
               laArticles.Take(3).ToList();


            return View(item);
        }
    }
}
