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

    public class UserTokenMap : IEntityTypeConfiguration<TUserToken>
    {
        public void Configure(EntityTypeBuilder<TUserToken> b)
        {
            // Composite primary key consisting of the UserId, LoginProvider and Name
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(t => t.LoginProvider);
            b.Property(t => t.Name);

            // Maps to the AspNetUserTokens table
            b.ToTable("AspNetUserTokens");  

        }
    }
}
