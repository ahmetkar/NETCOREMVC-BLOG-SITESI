using AutoMapper;
using Blog.Entity.DTOs.Message;
using Blog.Entity.DTOs.Subscriber;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System;
using System.Threading.Tasks;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ISettingService _settingService;
        private readonly IMessagesService _messagesService;
        private readonly ISubscriberService _subscriberService;

        public PublicController(
            IArticleService articleService,
            ICategoryService categoryService,
            IMapper mapper,
            IUserService userService,
            ISettingService settingService,
            IMessagesService messagesService,
            ISubscriberService subscriberService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _mapper = mapper;
            _userService = userService;
            _settingService = settingService;
            _messagesService = messagesService;
            _subscriberService = subscriberService;
        }

        [HttpPost("newsletter/subscribe")]
        [EnableRateLimiting("PublicLimitPolicy")]
        public async Task<IActionResult> SubscribeNewsletter([FromBody] CreateSubscribeModel request)
        {
            var ipAddress = HashHelper.Hash(HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString());
            var result = await _subscriberService.SubscribeAsync(request.Email, ipAddress);
            if (result)
            {
                return Ok(new { success = true, message = "Bültenimize başarıyla abone oldunuz." });
            }
            return BadRequest(new { success = false, message = "Geçersiz e-posta adresi." });
        }

        [HttpGet("newsletter/unsubscribe/validate")]
        public async Task<IActionResult> ValidateNewsletterUnsubscribe(Guid id, string token)
        {
            if (!await _subscriberService.IsUnsubscribeTokenValidAsync(id, token))
            {
                return BadRequest(new { success = false, message = "Geçersiz abonelik bağlantısı." });
            }

            return Ok(new { success = true });
        }

        [HttpPost("newsletter/unsubscribe")]
        [EnableRateLimiting("PublicLimitPolicy")]
        public async Task<IActionResult> UnsubscribeNewsletter([FromBody] NewsletterUnsubscribeModel request)
        {
            var result = await _subscriberService.UnsubscribeAsync(request.Id, request.Token);
            if (!result)
            {
                return BadRequest(new { success = false, message = "Abonelik işlemi gerçekleştirilemedi." });
            }

            return Ok(new { success = true, message = "Abonelikten başarıyla çıktınız." });
        }


        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("articles-minimal")]
        public async Task<IActionResult> GetAllArticlesMinimal()
        {
            var articles = await _articleService.GetAllArticlesWithCategoryForIndexAsync();
            var minimal = articles.Select(a => new { a.Id, a.Title });
            return Ok(minimal);
        }

        [HttpGet("articles")]
        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetAllArticleByPagingAsync();
            return Ok(articles);
        }

        [HttpGet("articles/paged")]
        public async Task<IActionResult> GetNewBlogs([FromQuery] int count, [FromQuery] int pageSize = 5)
        {
            var articles = await _articleService.GetAllArticleByPagingAsync(count + 1, pageSize, false);
            if (articles != null)
            {
                return Ok(articles);
            }
            return NotFound(new { message = "Başka makale yok" });
        }

        [HttpGet("pages/iletisim")]
        public async Task<IActionResult> Iletisim()
        {
            var icerik = await _settingService.GetPageAsync("iletisim");
            return Ok(icerik);
        }

        [HttpPost("iletisim")]
        [EnableRateLimiting("PublicLimitPolicy")]
        public async Task<IActionResult> Iletisim([FromBody] MessageCreateModel msg)
        {
            var add = await _messagesService.AddMessageAsync(msg);
            if (add != null)
            {
                return Ok(new { success = true, message = "Mesaj başarıyla gönderildi." });
            }
            return BadRequest(new { success = false, message = "Mesaj gönderilemedi." });
        }

        [HttpGet("pages/hakkimizda")]
        public async Task<IActionResult> Hakkimizda()
        {
            var icerik = await _settingService.GetPageAsync("hakkimizda");
            return Ok(icerik);
        }

        [HttpGet("pages/politikalarimiz")]
        public async Task<IActionResult> Politikalarimiz()
        {
            var icerik = await _settingService.GetPageAsync("politikalarimiz");
            return Ok(icerik);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 3, [FromQuery] bool isAscending = false)
        {
            var articles = await _articleService.SearchAsync(keyword, currentPage, pageSize, isAscending);
            return Ok(new { articles, keyword });
        }

        [HttpGet("blog/{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            var article = await _articleService.GetArticleBySlugWithCategoryAndImageAndUserAsync(slug);
            if (article == null) return NotFound();

            await _articleService.UpdateArticleViewCount(article.Id);
            
            var prevandnextArticles = await _articleService.GetPrevAndNextArticlesAsync(article.CreatedDate);
            var maylikeArticles = await _articleService.GetMayLikeArticlesAsync(article.Id);

            string ip = HashHelper.Hash(HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()) ?? "";

            return Ok(new {
                Article = article,
                PrevArticle = prevandnextArticles.Item1,
                NextArticle = prevandnextArticles.Item2,
                MayLikeArticles = maylikeArticles,
                UserIp = ip
            });
        }

        [HttpGet("category/{slug}")]
        public async Task<IActionResult> CategoryDetail(string slug, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 8, [FromQuery] bool isAscending = false)
        {
            var categoryInfo = await _categoryService.GetCategoryBySlugAsync(slug);
            if (categoryInfo == null) return NotFound();

            var articleByCategory = await _articleService.GetArticlesByCategoryAsync(categoryInfo.Id, currentPage, pageSize, isAscending);

            return Ok(new {
                Category = categoryInfo,
                Articles = articleByCategory
            });
        }

        [HttpGet("author/{authorName}")]
        public async Task<IActionResult> AuthorDetail(string authorName, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 4, [FromQuery] bool isAscending = false)
        {
            var authorDetails = await _userService.GetUserByNameAsync(authorName);
            if (authorDetails != null)
            {
                var articles = await _articleService.GetAuthorsArticleAsync(authorDetails.Id, currentPage, pageSize, isAscending);
                return Ok(new {
                    Author = authorDetails,
                    Articles = articles
                });
            }
            return NotFound();
        }


        [HttpPost("article/comment")]
        [EnableRateLimiting("PublicLimitPolicy")]
        public async Task<IActionResult> AddComment([FromForm] string id, [FromForm] string name, [FromForm] string text, [FromForm] string email)
        {
            if (!Guid.TryParse(id, out Guid articleId))
            {
                return BadRequest(new { success = false, message = "Geçersiz ID" });
            }

            var addComment = await _articleService.MakeComment(articleId, name, text, email);
            if (addComment.Item1 && addComment.Item2 && addComment.Item3)
            {
                return Ok(new { success = true, message = "Yorumunuz başarıyla eklendi" });
            }
            
            string message = "Yorumunuz gönderilemedi.";
            if (!addComment.Item2) message += " 3 ten fazla yorum yapamazsınız.";
            if (!addComment.Item3) message += " Geçerli bir email giriniz.";

            return BadRequest(new { success = false, message = message });
        }

        [HttpGet("layout-data")]
        public async Task<IActionResult> LayoutData()
        {
            var settings = await _settingService.GetSettings();

            var allCategories = await _categoryService.GetAllCategoriesAsync();
            var allArticlesForNav = await _articleService.GetAllArticlesWithCategoryForIndexAsync();
            
            // Get latest articles for hero section
            var latestArticles = await _articleService.GetLastFiveArticlesAsync();

            // Fetch exactly the 5 selected categories from settings (in order)
            var selectedCategoryIds = new List<Guid?> {
                settings.Category1Id,
                settings.Category2Id,
                settings.Category3Id,
                settings.Category4Id,
                settings.Category5Id
            }.Where(id => id.HasValue && id.Value != Guid.Empty).Select(id => id.Value).ToList();

            var filteredCategories = allCategories
                .Where(c => selectedCategoryIds.Contains(c.Id))
                .OrderBy(c => selectedCategoryIds.IndexOf(c.Id))
                .ToList();

            var categoriesWithArticles = filteredCategories.Select(c => new
            {
                c.Id,
                c.Name,
                c.Slug,
                Articles = allArticlesForNav.Where(a => a.Category?.Id == c.Id).OrderByDescending(a => a.CreatedDate).Take(4)
            });

            var navCategories = allCategories.Select(c => new
            {
                c.Id,
                c.Name,
                c.Slug
            });

            var footerCategories = allCategories.Take(5).ToList();

            // Sidebar items
            var mostVisited = await _articleService.GetMostVisitedArticlesAsync();
            var mostCommented = await _articleService.GetMostCommentedArticlesAsync();

            // Hero and Featured Articles
            var heroArticle = allArticlesForNav.FirstOrDefault(a => a.Id == settings.HeroArticleId) ?? latestArticles.FirstOrDefault();

            var featuredArticles = new List<Blog.Entity.DTOs.Article.ArticleIndexViewModel>();
            if (settings.FeaturedArticle1Id.HasValue && settings.FeaturedArticle1Id != Guid.Empty)
            {
                var f1 = allArticlesForNav.FirstOrDefault(a => a.Id == settings.FeaturedArticle1Id);
                if (f1 != null) featuredArticles.Add(f1);
            }
            if (settings.FeaturedArticle2Id.HasValue && settings.FeaturedArticle2Id != Guid.Empty)
            {
                var f2 = allArticlesForNav.FirstOrDefault(a => a.Id == settings.FeaturedArticle2Id);
                if (f2 != null) featuredArticles.Add(f2);
            }

            // Fallback for featured articles if not selected
            if (featuredArticles.Count < 2)
            {
                var fallbackArticles = latestArticles.Where(a => a.Id != heroArticle?.Id && !featuredArticles.Any(f => f.Id == a.Id)).Take(2 - featuredArticles.Count);
                featuredArticles.AddRange(fallbackArticles);
            }

            return Ok(new
            {
                Settings = settings,
                NavCategories = navCategories,
                FooterCategories = footerCategories,
                Categories = categoriesWithArticles, // We can keep this or remove it later if frontend completely switches to lazy
                TopArticles = latestArticles,
                SidebarTopArticles = mostVisited.Take(5),
                SidebarMostCommented = mostCommented.Take(5),
                HeroArticle = heroArticle,
                FeaturedArticles = featuredArticles
            });
        }

        [HttpGet("categories-lazy")]
        public async Task<IActionResult> CategoriesLazy([FromQuery] int skip = 0, [FromQuery] int take = 2)
        {
            var settings = await _settingService.GetSettings();
            var allCategories = await _categoryService.GetAllCategoriesAsync();
            var allArticlesForNav = await _articleService.GetAllArticlesWithCategoryForIndexAsync();

            var selectedCategoryIds = new List<Guid?> {
                settings.Category1Id,
                settings.Category2Id,
                settings.Category3Id,
                settings.Category4Id,
                settings.Category5Id
            }.Where(id => id.HasValue && id.Value != Guid.Empty).Select(id => id.Value).ToList();

            var filteredCategories = allCategories
                .Where(c => selectedCategoryIds.Contains(c.Id))
                .OrderBy(c => selectedCategoryIds.IndexOf(c.Id))
                .Skip(skip)
                .Take(take)
                .ToList();

            var categoriesWithArticles = filteredCategories.Select(c => new
            {
                c.Id,
                c.Name,
                c.Slug,
                Articles = allArticlesForNav.Where(a => a.Category?.Id == c.Id).OrderByDescending(a => a.CreatedDate).Take(4)
            });

            return Ok(new { success = true, categories = categoriesWithArticles });
        }
    }
}
