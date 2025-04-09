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

    public class RoleMap : IEntityTypeConfiguration<TRole>
    {
        public void Configure(EntityTypeBuilder<TRole> b)
        {
            // Primary key
            b.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            b.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.Name).HasMaxLength(256);
            b.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            b.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            b.HasMany<TRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            TRole superadmin = new TRole
            {
                Id = Guid.Parse("{0e4520f6-4144-4b4c-9724-c651046ee24a}"),
                Name = "Superadmin",
                NormalizedName = "SUPERADMIN",
            };
            TRole admin = new TRole{
                Id = Guid.Parse("{dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88}"),
                Name = "Admin",
                NormalizedName = "ADMIN",
            };

            TRole user = new TRole
            {
                Id = Guid.Parse("{b20731ee-51ed-4849-8d24-82d4a998edda}"),
                Name = "User",
                NormalizedName = "USER",
            };

            superadmin.ConcurrencyStamp = Guid.NewGuid().ToString();
            admin.ConcurrencyStamp = Guid.NewGuid().ToString();
            user.ConcurrencyStamp = Guid.NewGuid().ToString();

            b.HasData(
                superadmin,admin,user
            );
        }
    }
}
