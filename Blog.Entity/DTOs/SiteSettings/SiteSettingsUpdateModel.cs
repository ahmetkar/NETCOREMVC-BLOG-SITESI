using Blog.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOs.SiteSettings
{
    public class SiteSettingsUpdateModel
    {
        public string SiteTitle { get; set; }
        public Image LogoImage { get; set; }
        public IFormFile LogoFile { get; set; }


        public Guid? Category1Id { get; set; }
        public Guid? Category2Id { get; set; }
        public Guid? Category3Id { get; set; }
        public Guid? Category4Id { get; set; }
        public Guid? Category5Id { get; set; }

        public Guid? HeroArticleId { get; set; }
        public Guid? FeaturedArticle1Id { get; set; }
        public Guid? FeaturedArticle2Id { get; set; }

        public string FooterDescription { get; set; }

        public Image FooterLogo { get; set; }
        public IFormFile FooterLogoFile { get; set; }

        public Image AdminPanelLogo { get; set; }
        public IFormFile AdminPanelLogoFile { get; set; }

        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string Twitterurl { get; set; }
        public string? Youtubeurl { get; set; }

        public bool IsAIEnabled { get; set; }

        // Hakkımızda (About Us) Fields
        public string? AboutUsTitle { get; set; }
        public string? AboutUsDescription { get; set; }
        public string? AboutUsSectionTitle { get; set; }
        public string? AboutUsSectionDescription { get; set; }
        
        public string? AboutUsCard1Title { get; set; }
        public string? AboutUsCard1Description { get; set; }
        
        public string? AboutUsCard2Title { get; set; }
        public string? AboutUsCard2Description { get; set; }
        
        public string? AboutUsCard3Title { get; set; }
        public string? AboutUsCard3Description { get; set; }

        // İletişim (Contact) Fields
        public string? ContactEmail { get; set; }
        public string? ContactTitle { get; set; }
        public string? ContactDescription { get; set; }
    }
}
