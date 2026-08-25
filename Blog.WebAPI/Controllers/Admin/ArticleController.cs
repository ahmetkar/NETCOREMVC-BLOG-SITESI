using Blog.Entity.DTOs.Article;
using Blog.Service.Services.Abstractions;
using Blog.Web.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _configuration;
        private readonly ISubscriberService _subscriberService;
        public ArticleController(IArticleService articleService, ICategoryService categoryService,ISubscriberService subscriberService,IConfiguration configuration)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _subscriberService = subscriberService;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetAllArticlesWithCategoryAsync();
            return Ok(articles);
        }

        [HttpGet("categories")]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> Add([FromForm] ArticleCreateModel articleCreate)
        {
            if (ModelState.IsValid)
            {
                var siteName = _configuration["SiteName"];
                var siteLink = _configuration["SiteBlogLink"];
                await _articleService.CreateArticleAsync(articleCreate);
                var LastArticle = await _articleService.GetLastArticle();

             
                var bodytitle =  $"Merhaba değerli abonemiz websitemize {LastArticle.Title} adlı yeni  blog eklenmiştir.";
                var bodycontent = $"Merhaba değerli abonemiz websitemize {LastArticle.Title} adlı yeni  blog eklenmiştir.\n \n Blog hakkında açıklama : {articleCreate.Description}";
                var bodylink = $"{siteLink}/{LastArticle.Slug}";
                
                                
                await _subscriberService.SendAllSubscriberToNew($"{siteName} ' den {articleCreate.Title} başlıklı yeni blog.",bodytitle,bodycontent,bodylink);

                return Ok(new { success = true, message = "Makale başarıyla eklendi." });
            }
            return BadRequest(new { success = false, message = "Form verileri geçersiz." });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> GetArticleForUpdate(Guid id)
        {
            var article = await _articleService.GetArticleWithCategoryAndImageAsync(id);
            if (article != null)
            {
                return Ok(article);
            }
            return NotFound();
        }

        [HttpPut]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> Update([FromForm] ArticleUpdateModel updateModel)
        {
            if (ModelState.IsValid)
            {
                var title = await _articleService.UpdateArticleAsync(updateModel);
                return Ok(new { success = true, message = $"'{title}' başarıyla güncellendi." });
            }
            return BadRequest(new { success = false, message = "Form verileri geçersiz." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var title = await _articleService.SafeDeleteArticleAsync(id);
            return Ok(new { success = true, message = $"'{title}' başarıyla çöp kutusuna taşındı." });
        }

        [HttpDelete("force/{id}")]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> ForceDelete(Guid id)
        {
            var title = await _articleService.ForceDeleteArticleAsync(id);
            return Ok(new { success = true, message = $"'{title}' kalıcı olarak silindi." });
        }

        [HttpPut("undo/{id}")]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            var title = await _articleService.UndoDeleteArticleAsync(id);
            return Ok(new { success = true, message = $"'{title}' başarıyla geri getirildi." });
        }

        [HttpGet("deleted")]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> DeletedArticles()
        {
            var articles = await _articleService.GetAllDeletedArticles();
            return Ok(articles);
        }

        [HttpGet("comments")]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> Comments()
        {
            var comments = await _articleService.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpDelete("comment/{articleId}/{commentId}")]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> DeleteComment(Guid articleId, Guid commentId)
        {
            var delete = await _articleService.ForceDeleteCommentAsync(articleId, commentId);
            if (delete != null)
            {
                return Ok(new { success = true, message = $"{delete} adlı kişinin yorumu silindi." });
            }
            return BadRequest(new { success = false, message = "Yorum silinemedi." });
        }

        [HttpPut("comment/{articleId}/{commentId}")]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Editor},{RoleConsts.User}")]
        public async Task<IActionResult> ApproveComment(Guid articleId, Guid commentId, [FromQuery] string approved)
        {
            bool approve = bool.Parse(approved);
            var doApprove = await _articleService.ApproveCommentAsync(articleId, commentId, approve);
            if (doApprove != null)
            {
                if (approve)
                {
                    return Ok(new { success = true, message = $"{doApprove} adlı kişinin yorumu onaylandı." });
                }else
                {
                    return Ok(new { success = true, message = $"{doApprove} adlı kişinin yorumu onayı kaldırıldı." });
                }
                
            }
            return BadRequest(new { success = false, message = "Yorum onaylanamadı." });
        }
    }
}
