using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOs.Article
{
    public class ArticleFeatures
    {
        public float[] TitleFeatures { get; set; }
        public float[] DescFeatures { get; set; }
        public float[] AuthorFeatures { get; set; }
        public float[] CategoryFeatures { get; set; }
    }
}
