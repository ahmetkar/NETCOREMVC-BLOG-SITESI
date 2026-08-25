using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LayoutController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ISettingService _settingService;
        private readonly ICategoryService _categoryService;

        public LayoutController(IArticleService articleService, ISettingService settingService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _settingService = settingService;
            _categoryService = categoryService;
        }

        [HttpGet("sidebar")]
        public async Task<IActionResult> GetSidebar()
        {
            var setting = await _settingService.GetSettings();

            var topitem = await _articleService.GetMostVisitedArticlesAsync();
            var bottomitem = await _articleService.GetMostCommentedArticlesAsync();

            var articlesettings = new Dictionary<string, string>
            {
                { "top", "most-viewed" },
                { "bottom", "most-commented" }
            };

            var socialmedias = new Dictionary<string, string>
            {
                { "instagram", setting.InstagramUrl },
                { "twitter", setting.Twitterurl },
                { "youtube", setting.Youtubeurl },
                { "facebook", setting.FacebookUrl }
            };

            return Ok(new { TopItems = topitem, BottomItems = bottomitem, ArticleSettings = articlesettings, SocialMedias = socialmedias });
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTop()
        {
            var laArticles = await _articleService.GetLastFiveArticlesAsync();
            var item = laArticles.Take(3).ToList();
            return Ok(item);
        }

        [HttpGet("footer")]
        public async Task<IActionResult> GetFooter()
        {
            var settings = await _settingService.GetSettings();
            var articles = await _articleService.GetLastFiveArticlesAsync();
            var categories = await _categoryService.GetPrimeCategoriesAsync();
            return Ok(new { Settings = settings, Articles = articles, Categories = categories });
        }

        [HttpGet("header")]
        public async Task<IActionResult> GetHeader()
        {
            var settings = await _settingService.GetSettings();
            return Ok(settings);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var articles = await _articleService.GetAllArticlesWithCategoryForIndexAsync();

            var result = categories.Select(c => new
            {
                c.Id,
                c.Name,
                Articles = articles.Where(a => a.Category?.Id == c.Id).OrderByDescending(a => a.CreatedDate).Take(4)
            });

            return Ok(result);
        }
        
        [HttpGet("prime-categories")]
        public async Task<IActionResult> GetPrimeCategories()
        {
             var categories = await _categoryService.GetPrimeCategoriesAsync();
             return Ok(categories);
        }
    }
}
