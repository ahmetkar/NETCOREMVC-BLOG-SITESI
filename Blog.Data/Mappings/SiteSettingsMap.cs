using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class SiteSettingsMap : IEntityTypeConfiguration<SiteSettings>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SiteSettings> builder)
        {
            builder.HasOne(s => s.LogoImage)
            .WithMany()  // Many-to-one ilişki
            .HasForeignKey(s => s.LogoImageId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.FooterLogo)
           .WithMany()  // Many-to-one ilişki
           .HasForeignKey(s => s.FooterLogoId)
           .OnDelete(DeleteBehavior.NoAction);

            
            builder.HasData(new SiteSettings
            {
                SiteTitle = "Blog Web",
                CreatedBy = "deneme@gmail.com",
                CreatedDate = new DateTime(2023, 5, 15, 7, 0, 0),
                FacebookUrl = "https://facebook.com/",
                InstagramUrl = "https://instagram.com/",
                Youtubeurl = "https://youtube.com",
                Twitterurl = "https://twitter.com",
                IsAIEnabled = true,
                SidebarBottomArticleSetting = "most-commented",
                SidebarTopArticleSetting = "most-viewed",
                TopArticleSetting = "last-added",
                FooterDescription = "Techblog is a blog site.",
                FooterLogoId = Guid.Parse("{57597e1f-9547-423c-9485-ad7a390bd5a6}"),
                LogoImageId = Guid.Parse("{e30e3542-ca2a-4f87-8366-65fd3e287a7d}"),
                AdminPanelLogoId = Guid.Parse("{57597e1f-9547-423c-9485-ad7a390bd5a6}")
            });


        }
    }
}