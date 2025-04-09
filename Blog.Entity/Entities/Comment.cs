using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class Comment : EntityBase
    {
        public Comment() { }

        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
        public string CommentText {  get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsAprroved { get; set; }
    }
}
