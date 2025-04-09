using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class Page : EntityBase
    {
        public string PageTitle { get; set; }
        public string PageContent { get; set; } = string.Empty;
    }
}
