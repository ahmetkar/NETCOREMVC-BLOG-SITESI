using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class Article : EntityBase
    {
       
        public Article() { }
        public Article(string title,string description, string content,Guid CategoryId,Guid UserId,Guid ImageId,string createdBy) {
            this.Title = title;
            this.Description = description;
            this.CategoryID = CategoryId;
            this.UserId = UserId;
            this.ImageId = ImageId;
            this.Content = content;
            this.CreatedBy = createdBy;
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Content { get; set; }

        public int ViewCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;

        public Guid CategoryID { get; set; }

        public Category Category { get; set; }

        public Guid? ImageId  { get; set; }
        public Image Image { get; set; }

        public Guid UserId { get; set; }
        public TUser User { get; set; }

        public ICollection<ArticleVisitor> ArticleVisitors { get; set; }
      

        public ICollection<ArticleSimilarity> BaseArticleSimilarities { get; set; }
        public ICollection<ArticleSimilarity> TargetArticleSimilarities { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
