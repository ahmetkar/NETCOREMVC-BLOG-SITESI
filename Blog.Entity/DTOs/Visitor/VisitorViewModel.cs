using Blog.Core.Entities;
using Blog.Entity.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOs
{
    public class VisitorViewModel
    {
       
        public int Id { get; set; }
        public string IpAdress {  get; set; }
        public string UserAgent { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<CommentViewModel> Comments  { get; set; }

    }
}
