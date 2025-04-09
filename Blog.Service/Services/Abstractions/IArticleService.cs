using Blog.Entity.DTOs.Article;
using Blog.Entity.DTOs.Comment;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface IArticleService
    {
        public Task<List<ArticleViewModel>> GetAllArticlesWithCategoryAsync();
        public Task<List<ArticleViewModel>> GetAllDeletedArticles();
        Task CreateArticleAsync(ArticleCreateModel articleCreateModel, bool isAIActive = true);
        Task<string> SafeDeleteArticleAsync(Guid articleId);
        Task<string> UndoDeleteArticleAsync(Guid articleId);
        Task<ArticleViewModel> GetArticleWithCategoryAndImageAsync(Guid articleId);
        Task<string> UpdateArticleAsync(ArticleUpdateModel articleUpdateModel);
        Task<List<ArticleIndexViewModel>?> GetAllArticleByPagingAsync(int currentPage = 1, int pageSize = 5, bool isAscending = false);
        Task<ArticleListViewModel> GetAllByPagingAsync(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false);
        Task<ArticleListViewModel> SearchAsync(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false);
        Task<ArticleViewModel> GetArticleWithCategoryAndImageAndUserAsync(Guid articleId);
        Task<ArticleListViewModel> GetArticlesByCategoryAsync(Guid categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false);
        Task<List<ArticleIndexViewModel>> GetAllArticlesWithCategoryForIndexAsync();
        Task UpdateArticleViewCount(Guid id);
        Task<List<ArticleIndexViewModel>> GetMostVisitedArticlesAsync();
        Task<List<ArticleIndexViewModel>> GetMostCommentedArticlesAsync();
        Task<List<ArticleViewModel>> GetMayLikeArticlesAsync(Guid articleId);
        Task<(ArticleViewModel, ArticleViewModel)> GetPrevAndNextArticlesAsync(DateTime articleDate);
        Task<string> ForceDeleteArticleAsync(Guid articleId);
        Task<ArticleListViewModel> GetAuthorsArticleAsync(Guid authorId, int currentPage = 1, int pageSize = 8, bool isAscending = false);
        Task<(bool, bool, bool)> MakeComment(Guid articleId, string name, string text, string email);
        Task<List<ArticleIndexViewModel>> GetLastFiveArticlesAsync();
        Task<List<CommentViewModel>> GetAllCommentsAsync();
        Task<string> ForceDeleteCommentAsync(Guid articleId, Guid commentId);
        Task<string?> ApproveCommentAsync(Guid articleId, Guid commentId, string approved);



    }
}
