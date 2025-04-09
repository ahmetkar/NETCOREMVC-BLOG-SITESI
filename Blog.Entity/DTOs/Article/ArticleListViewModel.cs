using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOs.Article
{
    public class ArticleListViewModel
    {
       public IList<Blog.Entity.Entities.Article> Articles { get; set; }
       public Guid? CategoryId { get; set; }
       public virtual int currentPage { get; set; } = 1;
       public virtual int pageSize { get; set; } = 3;
       public virtual int TotalCount { get; set; }
       public virtual int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount,pageSize));
       public virtual bool showProvious => currentPage > 1;
       public virtual bool showNext => currentPage < TotalPages;
        public virtual bool IsAscending { get; set; } = false;
    }
}
