using Blog.Entity.DTOs.Category;
using Blog.Entity.DTOs.Comment;
using Blog.Entity.DTOs.Images;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOs.Article
{
    public class ArticleViewModel
    {

        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public ImageViewModel Image { get; set; }
        public UserViewModel User { get; set; }
        public CategoryViewModel Category { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
     
        public ICollection<CommentViewModel> Comments { get; set; }

    }
}
