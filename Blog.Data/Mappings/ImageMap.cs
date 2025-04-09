using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
           
            builder.HasData(new Image { Id = Guid.Parse("{21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d}"), FileName = "defaults/user.png", FileType = "image/png", CreatedBy = "Sistem", CreatedDate = new DateTime(2023, 5, 15, 7, 0, 0), IsDeleted = false });

            builder.HasData(new Image { Id = Guid.Parse("{e30e3542-ca2a-4f87-8366-65fd3e287a7d}"), FileName = "site-images/tech-logo.png", FileType = "image/png", CreatedBy = "Sistem", CreatedDate = new DateTime(2023, 5, 15, 7, 0, 0), IsDeleted = false });

            builder.HasData(new Image { Id = Guid.Parse("{57597e1f-9547-423c-9485-ad7a390bd5a6}"), FileName = "site-images/tech-footer-logo.png", FileType = "image/png", CreatedBy = "Sistem", CreatedDate = new DateTime(2023, 5, 15, 7, 0, 0), IsDeleted = false });

        }
    }
}
