using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Entity.DTOs.Comment
{
    public class CommentViewModel
    {

        public Guid Id { get; set; }
        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; }
        public Guid ArticleId { get; set; }
        public Entities.Article Article { get; set; }
        public string CommentText { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAprroved { get; set; }

    }
}
