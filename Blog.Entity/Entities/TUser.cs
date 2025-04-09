using Blog.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class TUser : IdentityUser<Guid>,IEntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ImageId { get; set; } = Guid.Parse("{21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d}");
        public string Biography { get; set; }
        public Image Image { get; set; }

        public ICollection<Article> Articles { get; set; }

    }
}
