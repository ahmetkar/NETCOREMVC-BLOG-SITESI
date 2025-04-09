using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class ArticleSimilarity : IEntityBase
    {
        public Guid BaseArticleId { get; set; }
        public Article BaseArticle { get; set; }
        public Guid TargetArticleId { get; set; }
        public Article TargetArticle { get; set; }
        public double Similarity { get; set; }
    }
}
