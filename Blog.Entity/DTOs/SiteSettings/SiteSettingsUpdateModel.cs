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

        public string TopArticleSetting { get; set; }
        public string SidebarTopArticleSetting { get; set; }
        public string SidebarBottomArticleSetting { get; set; }

        public string FooterDescription { get; set; }

        public Image FooterLogo { get; set; }
        public IFormFile FooterLogoFile { get; set; }

        public Image AdminPanelLogo { get; set; }
        public IFormFile AdminPanelLogoFile { get; set; }

        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string Twitterurl { get; set; }
        public string Youtubeurl { get; set; }

        public bool IsAIEnabled { get; set; }
    }
}
