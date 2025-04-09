using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class Category : EntityBase
    {
        public Category()
        {
            
        }
        public Category(string Name,string createdBy,DateTime createdDate)
        {
            this.Name = Name;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
        }
        public string Name { get; set; }
        public bool isMenuCategory { get; set; } = false; 
        public bool isPrimeCategory { get; set; } = false;
        public ICollection<Article> Articles { get; set; }
    }
}
