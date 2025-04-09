using Blog.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class Image : EntityBase
    {
        public Image()
        {
            
        }

        public Image(string Name,string FileType,string createdBy)
        {
            this.FileName = Name;
            this.FileType = FileType;
            this.CreatedBy = createdBy;

        }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<TUser> Users { get; set; }


    }
}
