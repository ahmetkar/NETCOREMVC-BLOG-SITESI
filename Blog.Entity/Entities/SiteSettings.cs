using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class SiteSettings : EntityBase
    {
        public SiteSettings()
        {
            
        }
    
        public string SiteTitle { get; set; }

        public Guid LogoImageId { get; set; }
        public Image LogoImage { get; set; }

        public string TopArticleSetting {  get; set; }
        public string SidebarTopArticleSetting { get; set; }
        public string SidebarBottomArticleSetting { get; set; }

        public string FooterDescription { get; set; }
        
        public Guid FooterLogoId { get; set; }
        public Image FooterLogo {  get; set; }

        public Guid AdminPanelLogoId { get; set; }
        public Image AdminPanelLogo { get; set; }

        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string Twitterurl { get; set; }
        public string Youtubeurl { get; set; }

        public bool IsAIEnabled { get; set; }


    }
}
