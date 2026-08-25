using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{

    public class RoleClaimMap : IEntityTypeConfiguration<TRoleClaim>
    {
        public void Configure(EntityTypeBuilder<TRoleClaim> b)
        {
            // Primary key
            b.HasKey(rc => rc.Id);

            // Maps to the AspNetRoleClaims table
            b.ToTable("AspNetRoleClaims");

            b.HasData(
                new TRoleClaim { Id = 1, RoleId = Guid.Parse("0e4520f6-4144-4b4c-9724-c651046ee24a"), ClaimType = "Permission", ClaimValue = "Categories.View" },
                new TRoleClaim { Id = 2, RoleId = Guid.Parse("0e4520f6-4144-4b4c-9724-c651046ee24a"), ClaimType = "Permission", ClaimValue = "Users.View" },
                new TRoleClaim { Id = 3, RoleId = Guid.Parse("0e4520f6-4144-4b4c-9724-c651046ee24a"), ClaimType = "Permission", ClaimValue = "Settings.View" },
                
                new TRoleClaim { Id = 4, RoleId = Guid.Parse("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"), ClaimType = "Permission", ClaimValue = "Categories.View" },
                new TRoleClaim { Id = 5, RoleId = Guid.Parse("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"), ClaimType = "Permission", ClaimValue = "Settings.View" }
            );
        }
    }
}
