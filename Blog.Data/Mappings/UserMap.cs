using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<TUser>
    {
        public void Configure(EntityTypeBuilder<TUser> b)
        {
            // Primary key
            b.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // Maps to the AspNetUsers table
            b.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.UserName).HasMaxLength(100);
            b.Property(u => u.NormalizedUserName).HasMaxLength(100);
            b.Property(u => u.Email).HasMaxLength(150);
            b.Property(u => u.NormalizedEmail).HasMaxLength(150);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            b.HasMany<TUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            b.HasMany<TUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            b.HasMany<TUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            /*
            var superadmin = new TUser
            {
                Id = Guid.Parse("{69bffe8d-6794-45d2-bd0f-48599928cdee}"),
                UserName = "ahmetkar",
                NormalizedUserName = "AHMETKAR",
                Email = "ahmetkar2346@gmail.com",
                NormalizedEmail = "AHMETKAR2346@GMAIL.COM",
                PhoneNumber = "+905308152000",
                FirstName = "Ahmet",
                LastName = "Kar",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                ImageId = Guid.Parse("{21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d}"),
                Biography = "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd"
            };
            superadmin.SecurityStamp = Guid.NewGuid().ToString();
            superadmin.PasswordHash = CreatePasswordHash(superadmin,"123456");

            var admin = new TUser
            {
                Id = Guid.Parse("{258794ef-c8f5-4ab5-8818-bcfd3218036a}"),
                UserName = "mehmetoz",
                NormalizedUserName = "MEHMETOZ",
                Email = "ahmetkar2077@gmail.com",
                NormalizedEmail = "AHMETKAR2077@GMAIL.COM",
                PhoneNumber = "+905308142441",
                FirstName = "Mehmet",
                LastName = "Oz",
                PhoneNumberConfirmed = false,
                EmailConfirmed = true,
                ImageId = Guid.Parse("{21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d}"),
                Biography = "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd"
            };

            admin.SecurityStamp = Guid.NewGuid().ToString();
            admin.PasswordHash = CreatePasswordHash(admin,"1234567");

            var user = new TUser
            {
                Id = Guid.Parse("{070f54e2-bf16-4d50-bd52-a7f9cd87c479}"),
                UserName = "farukdemir",
                NormalizedUserName = "FARUKDEMIR",
                Email = "farukdemir123@gmail.com",
                NormalizedEmail = "FARUKDEMIR123@GMAIL.COM",
                PhoneNumber = "+905308132112",
                FirstName = "Faruk",
                LastName = "Demir",
                PhoneNumberConfirmed = false,
                EmailConfirmed = true,
                ImageId = Guid.Parse("{21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d}"),
                Biography = "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd"

            };
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = CreatePasswordHash(user, "12345");

            b.HasData(
                superadmin
                ,admin,
                user
            );

        }

        private string CreatePasswordHash(TUser user,string password) {
            var passwordHasher = new PasswordHasher<TUser>();
            return passwordHasher.HashPassword(user,password);
        }*/
        }
    }
}
