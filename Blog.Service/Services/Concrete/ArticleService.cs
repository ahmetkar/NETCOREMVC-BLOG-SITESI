using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Article;
using Blog.Entity.DTOs.Category;
using Blog.Entity.DTOs.Comment;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWorkk _unitOfWorkk;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IImageHelper imageHelper;
        private readonly IArticleAIService articleAIService;
        private readonly IImageService imageService;
        private readonly ClaimsPrincipal user;

        public ArticleService(IUnitOfWorkk unitOfWorkk, IMapper mapper, IHttpContextAccessor httpContextAccessor, IImageHelper imageHelper,
            IArticleAIService articleAIService,IImageService imageService)
        {

            _unitOfWorkk = unitOfWorkk;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.imageHelper = imageHelper;
            this.articleAIService = articleAIService;
            this.imageService = imageService;
            user = httpContextAccessor.HttpContext.User;
        }


     



        public async Task CreateArticleAsync(ArticleCreateModel articleCreateModel, bool isAIActive = true)
        {
            var userId = user.GetLoggedInUserId();
            var userEmail = user.GetLoggedInEmail();

            Guid imageId = await imageService.ImageUpload(articleCreateModel.Title, articleCreateModel.imageFile, ImageType.Article,
                userEmail);

            var article = new Article
            {
                Title = articleCreateModel.Title,
                Description = articleCreateModel.Description,
                Content = articleCreateModel.Content,
                CategoryID = Guid.Parse(articleCreateModel.CategoryId),
                UserId = userId,
                CreatedBy = userEmail,
                ImageId = imageId
            };
            await _unitOfWorkk.GetRepository<Article>().AddAsync(article);
            await _unitOfWorkk.SaveAsync();

            if (isAIActive) await articleAIService.CreateCosinesForArticleAsync(article.Id, article.CreatedDate);

        }

        public async Task<List<ArticleViewModel>> GetAllArticlesWithCategoryAsync()
        {
            var articles = await _unitOfWorkk.GetRepository<Article>().GetAllAsync(x => x.IsDeleted == false, x => x.Category);
            var map = mapper.Map<List<ArticleViewModel>>(articles);
            return map;
        }


        public async Task<List<ArticleIndexViewModel>> GetAllArticlesWithCategoryForIndexAsync()
        {
            var articles = await _unitOfWorkk.GetRepository<Article>().GetAllAsync(x => x.IsDeleted == false, x => x.Category, x => x.Image, x => x.User);
            var map = mapper.Map<List<ArticleIndexViewModel>>(articles);
            return map;
        }

       


        public async Task<List<ArticleIndexViewModel>> GetLastFiveArticlesAsync()
        {
            var articles = await _unitOfWorkk.GetRepository<Article>().GetAllWithLimitAsync(5, x => x.IsDeleted == false, true, x => x.CreatedDate, x => x.Category, x => x.Image);
            var map = mapper.Map<List<ArticleIndexViewModel>>(articles);
            return map;
        }


        public async Task<List<ArticleIndexViewModel>> GetMostCommentedArticlesAsync()
        {
            var articles = await _unitOfWorkk.GetRepository<Article>().GetAllWithLimitAsync(5, x => !x.IsDeleted, true, x => x.CommentCount, x => x.Image, x => x.Category, x => x.User);

            var map = mapper.Map<List<ArticleIndexViewModel>>(articles);
            return map;
        }


        public async Task<List<ArticleIndexViewModel>> GetMostVisitedArticlesAsync()
        {
            var articles = await _unitOfWorkk.GetRepository<Article>().GetAllAsync(x => x.IsDeleted == false, x => x.Category, x => x.Image, x => x.User);
            articles = articles.OrderByDescending(x => x.ViewCount).Take(5).ToList();
            var map = mapper.Map<List<ArticleIndexViewModel>>(articles);
            return map;
        }




        public async Task<List<ArticleViewModel>> GetMayLikeArticlesAsync(Guid articleId)
        {
            var list = new List<ArticleViewModel> { };

            var cosines = await _unitOfWorkk.GetRepository<ArticleSimilarity>().GetAllWithLimitAsync(2, x => x.BaseArticle.Id == articleId, true, x => x.Similarity, x => x.BaseArticle, x => x.TargetArticle);

            if (cosines.Count > 1)
            {
                foreach (var i in cosines)
                {
                    var map = mapper.Map<ArticleViewModel>(i.TargetArticle);
                    list.Add(map);
                }

                return list;
            }
            return null;
        }

        public async Task<(ArticleViewModel, ArticleViewModel)> GetPrevAndNextArticlesAsync(DateTime articleDate)
        {
            var prevArticle = await _unitOfWorkk.GetRepository<Article>().GetAllWithLimitAsync(1, x => !x.IsDeleted && x.CreatedDate < articleDate, true, x => x.CreatedDate, x => x.Image, x => x.User, x => x.Category);
            var nextArticle = await _unitOfWorkk.GetRepository<Article>().GetAllWithLimitAsync(1, x => !x.IsDeleted && x.CreatedDate > articleDate, false, x => x.CreatedDate, x => x.Image, x => x.User, x => x.Category);

            ArticleViewModel prevMap = null;
            ArticleViewModel nextMap = null;

            if (prevArticle.Count > 0)
            {
                prevMap = mapper.Map<ArticleViewModel>(prevArticle[0]);
            }
            if (nextArticle.Count > 0)
            {
                nextMap = mapper.Map<ArticleViewModel>(nextArticle[0]);
            }


            return (prevMap, nextMap);
        }



        public async Task<ArticleViewModel> GetArticleWithCategoryAndImageAsync(Guid articleId)
        {
            var article = await _unitOfWorkk.GetRepository<Article>().GetAsync(x => x.IsDeleted == false && x.Id == articleId, x => x.Category, i => i.Image);
            var map = mapper.Map<ArticleViewModel>(article);
            return map;
        }



        public async Task<ArticleViewModel> GetArticleWithCategoryAndImageAndUserAsync(Guid articleId)
        {
            var article = await _unitOfWorkk.GetRepository<Article>().GetAsync(x => x.IsDeleted == false && x.Id == articleId, x => x.Category, i => i.Image,
                u => u.User, ui => ui.User.Image, x => x.Comments);
            var map = mapper.Map<ArticleViewModel>(article);
            return map;
        }




        public async Task<string> UpdateArticleAsync(ArticleUpdateModel articleUpdateModel)
        {

            var userEmail = user.GetLoggedInEmail();
            var article = await _unitOfWorkk.GetRepository<Article>().GetAsync(x => x.IsDeleted == false && x.Id == articleUpdateModel.Id, x => x.Category, i => i.Image);

            if (articleUpdateModel.imageFile != null)
            {
                if (article.Image != null)
                {
                    imageHelper.Delete(article.Image.FileName);
                }
                Guid imageId = imageService.ImageUpload(articleUpdateModel.Title, articleUpdateModel.imageFile, ImageType.Article,
                userEmail).Result;

                article.ImageId = imageId;

            }

            article.Title = articleUpdateModel.Title;
            article.Content = articleUpdateModel.Content;
            article.CategoryID = articleUpdateModel.CategoryId;
            article.ModifiedBy = userEmail;
            article.ModifiedDate = DateTime.Now;
            await _unitOfWorkk.GetRepository<Article>().UpdateAsync(article);
            await _unitOfWorkk.SaveAsync();

            return article.Title;
        }




        public async Task<string> SafeDeleteArticleAsync(Guid articleId)
        {
            var userEmail = user.GetLoggedInEmail();
            var article = await _unitOfWorkk.GetRepository<Article>().GetByGuidAsync(articleId);
            article.IsDeleted = true;
            article.DeleteDate = DateTime.Now;
            article.DeletedBy = userEmail;
            await _unitOfWorkk.GetRepository<Article>().UpdateAsync(article);

            await imageService.SafeDeleteImageAsync(article.ImageId);

            await _unitOfWorkk.SaveAsync();

            return article.Title;
        }

        public async Task<string> ForceDeleteArticleAsync(Guid articleId)
        {
            var article = await _unitOfWorkk.GetRepository<Article>().GetAsync(x => x.Id == articleId, x => x.Image);
            await imageService.ForceDeleteImageAsync(article.Image);
            await _unitOfWorkk.GetRepository<Article>().DeleteAsync(article);
            await _unitOfWorkk.SaveAsync();
            return article.Title;
        }





        public async Task<List<ArticleViewModel>> GetAllDeletedArticles()
        {
            var articles = await _unitOfWorkk.GetRepository<Article>().GetAllAsync(x => x.IsDeleted, x => x.Category);
            var map = mapper.Map<List<ArticleViewModel>>(articles);
            return map;
        }

        public async Task<string> UndoDeleteArticleAsync(Guid articleId)
        {
            var userEmail = user.GetLoggedInEmail();
            var article = await _unitOfWorkk.GetRepository<Article>().GetByGuidAsync(articleId);
            article.IsDeleted = false;
            article.DeleteDate = null;
            article.DeletedBy = null;

            await _unitOfWorkk.GetRepository<Article>().UpdateAsync(article);

            await imageService.UndoDeleteImageAsync(article.ImageId);

            await _unitOfWorkk.SaveAsync();

            return article.Title;
        }




        public async Task<ArticleListViewModel> GetAuthorsArticleAsync(Guid authorId, int currentPage = 1, int pageSize = 4, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;

            var articles = await _unitOfWorkk.GetRepository<Article>().GetAllAsync(x => x.IsDeleted == false && x.User.Id == authorId, x => x.Category, i => i.Image,
                u => u.User, ui => ui.User.Image);
            var sortedArticles = isAscending ? articles.OrderBy(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();


            return new ArticleListViewModel
            {
                Articles = sortedArticles,
                currentPage = currentPage,
                pageSize = pageSize,
                TotalCount = articles.Count(),
                IsAscending = isAscending
            };
        }



        public async Task<ArticleListViewModel> GetAllByPagingAsync(Guid? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;

            var articles = categoryId == null
                ? await _unitOfWorkk.GetRepository<Article>().GetAllAsync(a => !a.IsDeleted, a => a.Category, i => i.Image, u => u.User)
                : await _unitOfWorkk.GetRepository<Article>().GetAllAsync(a => !a.IsDeleted && a.CategoryID == categoryId, x => x.Category, i => i.Image, u => u.User,x=>x.Comments);

            var sortedArticles = isAscending ? articles.OrderBy(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new ArticleListViewModel
            {
                Articles = sortedArticles,
                CategoryId = categoryId == null ? null : categoryId.Value,
                currentPage = currentPage,
                pageSize = pageSize,
                TotalCount = articles.Count(),
                IsAscending = isAscending
            };

        }

        public async Task<List<ArticleIndexViewModel>?> GetAllArticleByPagingAsync(int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;

            var articles = await _unitOfWorkk.GetRepository<Article>().GetAllAsync(a => !a.IsDeleted, x => x.Category, i => i.Image, u => u.User, x => x.Comments);
            var count = articles.Count;
            if (articles != null)
            {
                List<Article>? sortedArticles = new List<Article>();
                if ((currentPage+pageSize) < count) { 
                sortedArticles = isAscending ? articles.OrderBy(x => x.CreatedDate).Skip(currentPage - 1).Take(pageSize).ToList()
                    : articles.OrderByDescending(x => x.CreatedDate).Skip(currentPage - 1).Take(pageSize).ToList();
                   
                }else
                {
                 var newPageSize = (currentPage+pageSize)-count;
                 sortedArticles = isAscending ? articles.OrderBy(x => x.CreatedDate).Skip(currentPage - 1).Take(newPageSize).ToList()
                    : articles.OrderByDescending(x => x.CreatedDate).Skip(currentPage - 1).Take(newPageSize).ToList();
                }

                if (!sortedArticles.IsNullOrEmpty())
                {
                    var map = mapper.Map<List<ArticleIndexViewModel>>(sortedArticles);
                    return map;
                }
            }

            return null;
        }

        public async Task<ArticleListViewModel> SearchAsync(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {

            pageSize = pageSize > 20 ? 20 : pageSize;



            var articles = await _unitOfWorkk.GetRepository<Article>().GetAllAsync(a => !a.IsDeleted &&
            (a.Title.Contains(keyword) || (a.Description.Contains(keyword) || a.Category.Name.Contains(keyword))), a => a.Category, i => i.Image, u => u.User);


            var sortedArticles = isAscending ? articles.OrderBy(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new ArticleListViewModel
            {
                Articles = sortedArticles,
                currentPage = currentPage,
                pageSize = pageSize,
                TotalCount = articles.Count(),
                IsAscending = isAscending
            };

        }


        public async Task<ArticleListViewModel> GetArticlesByCategoryAsync(Guid categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;

            var articles = await _unitOfWorkk.GetRepository<Article>().GetAllAsync(x => x.IsDeleted == false && x.CategoryID == categoryId, x => x.Category, x => x.Image, x => x.User);

            var sortedArticles = isAscending ? articles.OrderBy(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new ArticleListViewModel
            {
                Articles = sortedArticles,
                currentPage = currentPage,
                pageSize = pageSize,
                TotalCount = articles.Count(),
                IsAscending = isAscending
            };
        }


        public async Task UpdateArticleViewCount(Guid id)
        {
            var ipAdress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            var getArticle = await _unitOfWorkk.GetRepository<Article>().GetAsync(x => x.Id == id);
            var visitor = await _unitOfWorkk.GetRepository<Visitor>().GetAsync(x => x.IpAdress == ipAdress);

            var articleVisitor = await _unitOfWorkk.GetRepository<ArticleVisitor>().CountAsync(x => x.Visitor.Id == visitor.Id
                && x.Article.Id == id
            , x => x.Visitor, y => y.Article);

            if (articleVisitor == 0)
            {
                await _unitOfWorkk.GetRepository<ArticleVisitor>().AddAsync(new ArticleVisitor() { ArticleId = id, VisitorId = visitor.Id });
                getArticle.ViewCount += 1;
                await _unitOfWorkk.GetRepository<Article>().UpdateAsync(getArticle);
                await _unitOfWorkk.SaveAsync();
            }
        }

        private string NormalizeText(string text)
        {
            text = Regex.Replace(text, "[\\/:*?\"<>|=]", " ").Trim();
            return text;
        }

        public async Task<(bool, bool, bool)> MakeComment(Guid articleId, string name, string text, string email)
        {

            (bool, bool, bool) results = (false, true, true);

            var ipAdress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            var getArticle = await _unitOfWorkk.GetRepository<Article>().GetAsync(x => x.Id == articleId);
            var visitor = await _unitOfWorkk.GetRepository<Visitor>().GetAsync(x => x.IpAdress == ipAdress);

            var isVisitorExists = await _unitOfWorkk.GetRepository<Comment>().GetAllAsync(x => x.VisitorId == visitor.Id && x.ArticleId == articleId);

            if (isVisitorExists.Count > 3)
            {
                results.Item2 = false;

            }

            string NName = NormalizeText(name);
            string NText = NormalizeText(text);
            string NEmail = NormalizeText(email);
           

            var emailAddress = new EmailAddressAttribute();

            if (!emailAddress.IsValid(NEmail)) results.Item3 = false;

            if (visitor != null)
            {
                await _unitOfWorkk.GetRepository<Comment>().AddAsync(new Comment() { CreatedBy = ipAdress, ArticleId = articleId, VisitorId = visitor.Id, CommentText = NText, Email = NEmail, Name = NName });
                getArticle.CommentCount += 1;
                await _unitOfWorkk.GetRepository<Article>().UpdateAsync(getArticle);
                await _unitOfWorkk.SaveAsync();
                results.Item1 = true;
            }

            return results;
        }




        public async Task<List<CommentViewModel>> GetAllCommentsAsync()
        {
            var comments = await _unitOfWorkk.GetRepository<Comment>().GetAllAsync(x => !x.IsDeleted, x => x.Article);
            var map = mapper.Map<List<CommentViewModel>>(comments);
            return map;
        }



        public async Task<string> ForceDeleteCommentAsync(Guid articleId, Guid commentId)
        {
            var comment = await _unitOfWorkk.GetRepository<Comment>().GetAsync(x => x.ArticleId == articleId && x.Id == commentId, x => x.Article);
            comment.Article.CommentCount = comment.Article.CommentCount - 1;
            await _unitOfWorkk.GetRepository<Article>().UpdateAsync(comment.Article);
            await _unitOfWorkk.GetRepository<Comment>().DeleteAsync(comment);
            await _unitOfWorkk.SaveAsync();
            return comment.Name;
        }


        public async Task<string?> ApproveCommentAsync(Guid articleId, Guid commentId, string approved)
        {
            bool? approve = bool.Parse(approved);

            if (approve != null)
            {
                var comment = await _unitOfWorkk.GetRepository<Comment>().GetAsync(x => x.IsDeleted == false && x.Id == commentId && x.ArticleId == articleId, x => x.Article);
                if (comment != null)
                {
                    if (approve.Value)
                    {
                        comment.IsAprroved = false;
                    }
                    else
                    {
                        comment.IsAprroved = true;
                    }

                    await _unitOfWorkk.GetRepository<Comment>().UpdateAsync(comment);
                    await _unitOfWorkk.SaveAsync();
                    return comment.Name;
                }
            }
            return null;
        }
        
    }
}
