using Blog.Entity.DTOs.Category;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOs.Article
{
    public class ArticleIndexViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Image Image { get; set; }
        public TUser User { get; set; }
        public CategoryViewModel Category { get; set; }
        public bool IsDeleted { get; set; }
        public int ViewCount { get; set; }
       

    }
}
