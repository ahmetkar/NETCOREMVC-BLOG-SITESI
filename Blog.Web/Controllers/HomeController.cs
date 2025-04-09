using System.Diagnostics;
using System.Text.Json;
using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Article;
using Blog.Entity.DTOs.Category;
using Blog.Entity.DTOs.Message;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using Blog.Web.Models;
using Blog.Web.ResultMessages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using NToastNotify.Helpers;


namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IUnitOfWorkk unitOfWorkk;
        private readonly IUserService userService;
        private readonly IToastNotification toast;
        private readonly ISettingService settingService;
        private readonly IMessagesService messagesService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public IArticleService _articleService { get; }

        public HomeController(IArticleService articleService,ICategoryService categoryService,IMapper mapper,IHttpContextAccessor httpContextAccessor,
            IUnitOfWorkk unitOfWorkk,IUserService userService,IToastNotification toast,ISettingService settingService,IMessagesService messagesService)
        {
            _articleService = articleService;
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.unitOfWorkk = unitOfWorkk;
            this.userService = userService;
            this.toast = toast;
            this.settingService = settingService;
            this.messagesService = messagesService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var articles = await _articleService.GetAllArticleByPagingAsync();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> GetNewBlogs(int count) 
        {
            int pageSize = 5;
            var getarticles = await _articleService.GetAllArticleByPagingAsync(count+1,pageSize,false);

          
                if (getarticles != null)
                {
                    return PartialView("_DataPartial", getarticles);
                }


            return Json(new { message ="Baþka makale yok"});

        }


       [HttpGet]
        public async Task<IActionResult> Iletisim()
        {

            var icerik = await settingService.GetPageAsync("iletisim");

            return View(icerik);
        }


        [HttpPost]
        public async Task<IActionResult> Iletisim(string name, string email, string tel, string subject, string body)
        {

            var msg = new MessageCreateModel { Name = name, Email = email, Body = body, Subject = subject, Tel = tel };

            var add = await messagesService.AddMessageAsync(msg);

            if (!add.Equals(null))
            {
                return Json(new { success = true,message="Mesaj baþarýyla gönderildi." });
            }
            else
            {
                return Json(new { success = false, message = "Mesaj gönderilemedi." });
            }
        }



        [HttpGet]
        public async Task<IActionResult> Hakkimizda()
        {

            var icerik = await settingService.GetPageAsync("hakkimizda");

            return View(icerik);
        }

        [HttpGet]
        public async Task<IActionResult> Politikalarimiz()
        {

            var icerik = await settingService.GetPageAsync("politikalarimiz");

            return View(icerik);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            var articles = await _articleService.SearchAsync(keyword, currentPage, pageSize, isAscending);

            return View((articles,keyword));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {


            await _articleService.UpdateArticleViewCount(id);
            
            var article = await _articleService.GetArticleWithCategoryAndImageAndUserAsync(id);

            var prevandnextArticles = await _articleService.GetPrevAndNextArticlesAsync(article.CreatedDate);

            var maylikeArticles = await _articleService.GetMayLikeArticlesAsync(id);

            ViewBag.MayLikeArticles = maylikeArticles;
            ViewBag.NextArticle = prevandnextArticles.Item2;
            ViewBag.PrevArticle = prevandnextArticles.Item1;
            var conn = httpContextAccessor.HttpContext?.Connection;
            if (conn != null) {
                TempData["myIp"] = conn.RemoteIpAddress?.MapToIPv4().ToString();
            }
            return View(article);
        }


        [HttpGet]
        public async Task<IActionResult> CategoryDetail(Guid category, int currentPage = 1, int pageSize = 8, bool isAscending = false)
        {
            var categoryInfo = await categoryService.GetCategoryByGuid(category);
            var cmap = mapper.Map<CategoryViewModel>(categoryInfo);
            var articleByCategory = await _articleService.GetArticlesByCategoryAsync(category,currentPage,pageSize,isAscending);

            return View((articleByCategory,category,cmap));
        }

        [HttpGet]
        public async Task<IActionResult> AuthorDetail(string authorName, int currentPage = 1, int pageSize =4 , bool isAscending = false)
        {
            var authorDetails = await userService.GetUserByNameAsync(authorName);
             
            if (authorDetails != null)
            {
                var authormap = mapper.Map<UserViewModel>(authorDetails);
                var articles = await _articleService.GetAuthorsArticleAsync(authormap.Id, currentPage, pageSize, isAscending);
                return View((articles,authormap));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string id,string name,string text,string email)
        {
            Guid articleId;
            if (!Guid.TryParse(id, out articleId))
            {
                return Json(new { success = false, message = "Hata oluþtu" });
            }

            (bool,bool,bool) addComment = await _articleService.MakeComment(articleId,name,text,email);
            string message = "";
            if (addComment.Item1 && addComment.Item2&& addComment.Item3)
            {
                message = "Yorumunuz baþarýyla eklendi";
                return Json(new { success = true, message = message });        ;  
            }
            else
            {
                message = "Yorumunuz gönderilemedi.";
                if (!addComment.Item2)
                {
                    message += " 3 ten fazla yorum yapamazsýnýz.";
                }
                else
                {
                    if (!addComment.Item3)
                    {
                        message += " Geçerli bir email giriniz. ";

                    }
                }

                return Json(new { success = false, message = message });

            }
        }






        }
}
