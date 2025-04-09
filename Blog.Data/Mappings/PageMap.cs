using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class PageMap : IEntityTypeConfiguration<Page>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Page> builder)
        {
            builder.HasData(new Page
            {
            Id = Guid.Parse("{a64419c7-c4e0-4501-92a7-42870acb95cf}"),
            PageTitle = "Hakkımızda",
            PageContent = "Hakkımızda içeriği eklenmedi.",
            }, new Page
            {
                Id = Guid.Parse("{254f012e-4f64-4410-a98d-720e35275c25}"),
                PageTitle = "İletişim",
                PageContent = "İletişim içeriği eklenmedi.",
            }, new Page
            {
                Id = Guid.Parse("{a6dcc8a8-0b2d-418b-851f-b30022908a90}"),
                PageTitle = "Politikalarımız",
                PageContent = "Politika içeriği eklenmedi.",
            });

        }
    }
}
