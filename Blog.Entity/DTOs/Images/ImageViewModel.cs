using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOs.Images
{
    public class ImageViewModel
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Blog.Entity.Entities.Article> Articles { get; set; }
        public ICollection<Blog.Entity.Entities.TUser> Users { get; set; }
    }
}
