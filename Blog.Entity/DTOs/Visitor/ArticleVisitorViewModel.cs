using Blog.Core.Entities;
using Blog.Entity.DTOs.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOs
{
    public class ArticleVisitorViewModel
    {
       
        public Guid ArticleId { get; set; }
        public ArticleViewModel Article { get; set; }

        public int VisitorId    { get; set; }
        public VisitorViewModel Visitor { get; set; }
    }
}
