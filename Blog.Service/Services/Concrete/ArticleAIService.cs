using Blog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Text;
using Blog.Entity.DTOs.Article;
using Blog.Entity.Entities;
using Blog.Data.UnitOfWorks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.Service.Services.Concrete
{
    public class ArticleAIService : IArticleAIService
    {
        private readonly IUnitOfWorkk _unitOfWorkk;

        public ArticleAIService(IUnitOfWorkk unitOfWorkk)
        {
            _unitOfWorkk = unitOfWorkk;
        }


        private async Task<(List<ArticleAIModel>,ArticleAIModel)> PrepareDatas(Guid articleId,DateTime? date)
        {
            List<Article> articles = await _unitOfWorkk.GetRepository<Article>().GetAllWithLimitAsync(50, x => x.IsDeleted == false && x.Id != articleId && x.CreatedDate <= date, true, x => x.CreatedDate,x=>x.Category,x=>x.User);


            

            var datas = articles.Select<Article,ArticleAIModel>(x => new ()
            {
                Id =x.Id.ToString(),
                Title = x.Title,
                Description = x.Description,
                FirstName = x.User.FirstName,
                CategoryName = x.Category.Name
            }).ToList();



            Article article = await _unitOfWorkk.GetRepository<Article>().GetAsync(x => x.Id == articleId, x => x.User, x => x.Category);

       

            ArticleAIModel baseData = new ArticleAIModel()
            {
                Id=article.Id.ToString(),
                Title = article.Title,
                Description = article.Description,
                FirstName = article.User.FirstName,
                CategoryName = article.Category.Name
            };

            return (datas,baseData);
        }

        public async Task CreateCosinesForArticleAsync(Guid articleId, DateTime? date)
        {

            var datasResult = await PrepareDatas(articleId, date);

            var datas = datasResult.Item1;
            var baseData = datasResult.Item2;

            Console.WriteLine();

            var result = CosineForArticle(datas, baseData);

            Console.WriteLine();

            foreach (var item in result)
            {
                Guid k = item.Key;
                double v = item.Value;

                ArticleSimilarity entity = new ArticleSimilarity { BaseArticleId = Guid.Parse(baseData.Id), TargetArticleId = k, Similarity = v };

                await _unitOfWorkk.GetRepository<ArticleSimilarity>().AddAsync(entity);
            }

            await _unitOfWorkk.SaveAsync();


        }


        private Dictionary<Guid,double> CosineForArticle(List<ArticleAIModel> articles,ArticleAIModel baseArticle)
        {

            var mlContext = new MLContext();
            articles.Add(baseArticle);


            var data = mlContext.Data.LoadFromEnumerable(articles);
            

            var titleTextPipeline = mlContext.Transforms.Text.FeaturizeText("TitleFeatures", nameof(ArticleAIModel.Title));
            var descTextPipeline = mlContext.Transforms.Text.FeaturizeText("DescFeatures", nameof(ArticleAIModel.Description));
            var authorTextPipeline = mlContext.Transforms.Text.FeaturizeText("AuthorFeatures", nameof(ArticleAIModel.FirstName));
            var catTextPipeline = mlContext.Transforms.Text.FeaturizeText("CategoryFeatures", nameof(ArticleAIModel.CategoryName));


            var combinedPipeline = titleTextPipeline
                .Append(descTextPipeline)
                .Append(authorTextPipeline)
                .Append(catTextPipeline);

         
            var model = combinedPipeline.Fit(data);

           
            var transformedData = model.Transform(data);

          
            var vectors = mlContext.Data.CreateEnumerable<ArticleFeatures>(transformedData, reuseRowObject: false).ToList();

            var baseArticleVector  = vectors[vectors.Count - 1];

            Dictionary<Guid, double> similarities = new Dictionary<Guid, double>();
            for (int i = 0; i < vectors.Count; i++)
            {
                if (i == vectors.Count-1) continue; 

                var similarity = CosineSimilarity(
                    baseArticleVector.TitleFeatures, vectors[i].TitleFeatures,
                    baseArticleVector.DescFeatures, vectors[i].DescFeatures,
                    baseArticleVector.AuthorFeatures, vectors[i].AuthorFeatures,
                    baseArticleVector.CategoryFeatures, vectors[i].CategoryFeatures);

                similarities.Add(Guid.Parse(articles[i].Id),similarity);
            }


            return similarities;


        }

        private double CosineSimilarity(float[] titleVec1, float[] titleVec2, float[] contentVec1, float[] contentVec2, float[] authorVec1, float[] authorVec2, float[] dateVec1, float[] dateVec2)
        {
            double titleSimilarity = CosineSimilarityForFeature(titleVec1, titleVec2);
            double contentSimilarity = CosineSimilarityForFeature(contentVec1, contentVec2);
            double authorSimilarity = CosineSimilarityForFeature(authorVec1, authorVec2);
            double dateSimilarity = CosineSimilarityForFeature(dateVec1, dateVec2);

            return (titleSimilarity + contentSimilarity + authorSimilarity + dateSimilarity) / 4;
        }

        private double CosineSimilarityForFeature(float[] vector1, float[] vector2)
        {
            var dotProduct = vector1.Zip(vector2, (x, y) => x * y).Sum();
            var norm1 = Math.Sqrt(vector1.Sum(x => x * x));
            var norm2 = Math.Sqrt(vector2.Sum(x => x * x));

            return dotProduct / (norm1 * norm2);
        }
    }
}

