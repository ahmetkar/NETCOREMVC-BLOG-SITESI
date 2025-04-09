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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category { Id = Guid.Parse("{58252b86-ec84-4c93-b3df-e9b1bf39d3bf}"), Name = "Asp.net", CreatedBy = "Admin test", CreatedDate = new DateTime(2023, 5, 15, 7, 0, 0), IsDeleted = false },
                new Category { Id = Guid.Parse("{80ac9637-35b9-42d1-afb7-297242e1e7c5}"), Name = "Visual studio 2022", CreatedBy = "Admin test", CreatedDate = new DateTime(2023, 5, 15, 7, 0, 0), IsDeleted = false }
                );
        } 
    }
}
