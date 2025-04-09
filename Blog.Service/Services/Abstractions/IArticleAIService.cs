using Blog.Entity.DTOs.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface IArticleAIService
    {
        Task CreateCosinesForArticleAsync(Guid articleId, DateTime? date);
    }
}
