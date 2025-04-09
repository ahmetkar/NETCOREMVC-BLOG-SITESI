using Blog.Entity.DTOs.Category;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOs.Article
{
    public class ArticleCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string CategoryId { get; set; }
       
        public IFormFile imageFile {  get; set; }


        public IList<CategoryViewModel> Categories { get; set; }

    }
}
