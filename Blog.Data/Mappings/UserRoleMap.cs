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
    public class UserRoleMap : IEntityTypeConfiguration<TUserRole>
    {
        public void Configure(EntityTypeBuilder<TUserRole> b)
        {
            // Primary key
            b.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            b.ToTable("AspNetUserRoles");

            b.HasData(
             new TUserRole
             {
                 UserId = Guid.Parse("{69bffe8d-6794-45d2-bd0f-48599928cdee}"),
                 RoleId = Guid.Parse("{0e4520f6-4144-4b4c-9724-c651046ee24a}")

             }, new TUserRole
             {
                 UserId = Guid.Parse("{258794ef-c8f5-4ab5-8818-bcfd3218036a}"),
                 RoleId = Guid.Parse("{dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88}")
             },new TUserRole
             {
                 UserId = Guid.Parse("{070f54e2-bf16-4d50-bd52-a7f9cd87c479}"),
                 RoleId = Guid.Parse("{b20731ee-51ed-4849-8d24-82d4a998edda}")
             }
                );
        }
    }
}
