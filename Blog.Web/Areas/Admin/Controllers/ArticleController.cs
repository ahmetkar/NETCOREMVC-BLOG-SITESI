using AutoMapper;
using Blog.Entity.DTOs.Article;
using Blog.Entity.Entities;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using Blog.Web.Consts;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area(RoleConsts.Admin)]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IValidator<Article> validator;
        private readonly IToastNotification toast;
       

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper, IValidator<Article> validator, IToastNotification toast)
        {
            this.articleService = articleService;
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.validator = validator;
            this.toast = toast;
            
        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> Index()
        {
            var articles = await articleService.GetAllArticlesWithCategoryAsync();
            
            return View(articles);
        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> Add()
        {
            
            var categories = await categoryService.GetAllCategoriesAsync();
            return View(new ArticleCreateModel { Categories = categories });
        }



        [HttpPost]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> Add(ArticleCreateModel articleCreate)
        {
            var map = mapper.Map<Article>(articleCreate);
            var result = await validator.ValidateAsync(map);
            if (result.IsValid)
            {
                await articleService.CreateArticleAsync(articleCreate);
                toast.AddSuccessToastMessage(Messages.Add(articleCreate.Title), new ToastrOptions { Title = "Başarılı" });

                return RedirectToAction("Index", "Article", new { Area = "Admin" });
            }
            else
            {
                //Hata verdiği durumda tekrar view dönmeli
                result.AddModelState(this.ModelState);
                var categories = await categoryService.GetAllCategoriesAsync();
                return View(new ArticleCreateModel { Categories = categories });
            }


        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Update(Guid articleId)
        {

            var article = await articleService.GetArticleWithCategoryAndImageAsync(articleId);
            var categories = await categoryService.GetAllCategoriesAsync();

            var articleUpdate = mapper.Map<ArticleUpdateModel>(article);
            articleUpdate.Categories = categories;

            return View(articleUpdate);
        }

        [HttpPost]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Update(ArticleUpdateModel updateModel)
        {

            var map = mapper.Map<Article>(updateModel);
            var result = await validator.ValidateAsync(map);
            if (result.IsValid)
            {
                var title = await articleService.UpdateArticleAsync(updateModel);
                toast.AddSuccessToastMessage(Messages.Update(title), new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index", new { Area = "Admin" });
            }
            else
            {
                result.AddModelState(this.ModelState);


            }

            var categories = await categoryService.GetAllCategoriesAsync();
            updateModel.Categories = categories;

            return View(updateModel);



        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Delete(Guid articleId)
        {
            var title = await articleService.SafeDeleteArticleAsync(articleId);
            toast.AddSuccessToastMessage(Messages.Delete(title), new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Index", "Article", new { Area = "Admin" });

        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> ForceDelete(Guid articleId)
        {
            var title = await articleService.ForceDeleteArticleAsync(articleId);
            toast.AddSuccessToastMessage(Messages.Delete(title), new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Index", "Article", new { Area = "Admin" });

        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> UndoDelete(Guid articleId)
        {
            var title = await articleService.UndoDeleteArticleAsync(articleId);
            toast.AddSuccessToastMessage(Messages.UndoDelete(title), new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Index", "Article", new { Area = "Admin" });

        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> DeletedArticles()
        {
            var articles = await articleService.GetAllDeletedArticles();
            return View(articles);
        }


        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Comments()
        {
            var comments = await articleService.GetAllCommentsAsync();

            return View(comments);

        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> DeleteComment(Guid articleId,Guid commentId)
        {

            var delete = await articleService.ForceDeleteCommentAsync(articleId,commentId);

            if (delete != null) {
                toast.AddSuccessToastMessage(delete+" adlı kişinin yorumu başarıyla silindi.", new ToastrOptions { Title = "Başarılı" });
            }
            else
            {
                toast.AddErrorToastMessage(delete + " adlı kişinin yorumu silinemedi.", new ToastrOptions { Title = "Başarısız" });
            }
            
            return RedirectToAction("Comments", "Article", new { Area = "Admin" });

        }

        [HttpGet]
        [Authorize(Roles = $" {RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> ApproveComment(Guid articleId, Guid commentId,string approved)
        {

            var approve = await articleService.ApproveCommentAsync(articleId,commentId,approved);
            if (approve != null)
            {
                toast.AddSuccessToastMessage(approve + " adlı kişinin yorumu başarıyla onaylandı.", new ToastrOptions { Title = "Başarılı" });
            }
            else
            {
                toast.AddErrorToastMessage(approve + " adlı kişinin yorumu onaylanamadı.", new ToastrOptions { Title = "Başarısız" });
            }

            return RedirectToAction("Comments", "Article", new { Area = "Admin" });

        }
    }
}
