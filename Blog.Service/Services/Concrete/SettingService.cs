using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Article;
using Blog.Entity.DTOs.Page;
using Blog.Entity.DTOs.SiteSettings;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Service.Services.Concrete
{ 
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWorkk unitOfWorkk;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IImageService imageService;
        private readonly ClaimsPrincipal user;

        public SettingService(IUnitOfWorkk unitOfWorkk, IHttpContextAccessor httpContextAccessor, IMapper mapper,IImageService imageService)
        {
            this.unitOfWorkk = unitOfWorkk;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.imageService = imageService;
            this.user = httpContextAccessor.HttpContext.User;
        }

        

        public async Task<Guid?> UpdateSettings(SiteSettingsUpdateModel updateSetting)
        {
            var userEmail = user.GetLoggedInEmail();

            var settings = await unitOfWorkk.GetRepository<SiteSettings>().GetAllWithLimitAsync(1, x => !x.IsDeleted,
                true, x => x.CreatedDate, x => x.LogoImage, x => x.FooterLogo, x => x.AdminPanelLogo);
            
            var setting = settings[0];
            if (setting != null) { 
           
            if (updateSetting.LogoFile != null)
            {
                Guid? logoImageId = await imageService.ImageUpload("logo", updateSetting.LogoFile, ImageType.Site, userEmail);
                setting.LogoImageId = logoImageId.Value;
            }

            if (updateSetting.FooterLogoFile != null)
            {
                Guid? footerlogoImageId = await imageService.ImageUpload("footer-logo", updateSetting.FooterLogoFile, ImageType.Site, userEmail);
                setting.FooterLogoId = footerlogoImageId.Value;
               
            }

                if (updateSetting.AdminPanelLogoFile != null)
                {
                    Guid? adminpanelImageId = await imageService.ImageUpload("admin-panel", updateSetting.AdminPanelLogoFile, ImageType.Site, userEmail);
                    setting.AdminPanelLogoId = adminpanelImageId.Value;

                }

                setting.Category1Id = updateSetting.Category1Id;
                setting.Category2Id = updateSetting.Category2Id;
                setting.Category3Id = updateSetting.Category3Id;
                setting.Category4Id = updateSetting.Category4Id;
                setting.Category5Id = updateSetting.Category5Id;

                setting.HeroArticleId = updateSetting.HeroArticleId;
                setting.FeaturedArticle1Id = updateSetting.FeaturedArticle1Id;
                setting.FeaturedArticle2Id = updateSetting.FeaturedArticle2Id;
                   
                setting.ModifiedBy = userEmail;
                setting.ModifiedDate = DateTime.Now;
                setting.SiteTitle = updateSetting.SiteTitle;
                
                // Hakkımızda mapping
                setting.AboutUsTitle = updateSetting.AboutUsTitle;
                setting.AboutUsDescription = updateSetting.AboutUsDescription;
                setting.AboutUsSectionTitle = updateSetting.AboutUsSectionTitle;
                setting.AboutUsSectionDescription = updateSetting.AboutUsSectionDescription;
                setting.AboutUsCard1Title = updateSetting.AboutUsCard1Title;
                setting.AboutUsCard1Description = updateSetting.AboutUsCard1Description;
                setting.AboutUsCard2Title = updateSetting.AboutUsCard2Title;
                setting.AboutUsCard2Description = updateSetting.AboutUsCard2Description;
                setting.AboutUsCard3Title = updateSetting.AboutUsCard3Title;
                setting.AboutUsCard3Description = updateSetting.AboutUsCard3Description;

                // İletişim mapping
                setting.ContactEmail = updateSetting.ContactEmail;
                setting.ContactTitle = updateSetting.ContactTitle;
                setting.ContactDescription = updateSetting.ContactDescription;
            
                // Diğer bilgiler
                setting.FacebookUrl = updateSetting.FacebookUrl;
                setting.InstagramUrl = updateSetting.InstagramUrl;
                setting.Twitterurl = updateSetting.Twitterurl;
                setting.Youtubeurl = updateSetting.Youtubeurl;
                setting.FooterDescription = updateSetting.FooterDescription;
                setting.IsAIEnabled = updateSetting.IsAIEnabled;
            
                    await unitOfWorkk.GetRepository<SiteSettings>().UpdateAsync(setting);
                    await unitOfWorkk.SaveAsync();
                    return setting.Id;
            }
            return null;
            
        }

        public async Task<SiteSettings?> GetSettings()
        {
           
            var settings = await unitOfWorkk.GetRepository<SiteSettings>().GetAllWithLimitAsync(1, x =>!x.IsDeleted,
                    true, x => x.CreatedDate, x => x.LogoImage, x => x.FooterLogo, x => x.AdminPanelLogo);

            if (settings.Count > 0)
            {
                return settings[0];
            }
            return null;
        }

        private string PageTitleReplace(string pageTitleRaw)
        {
            string pageTitle = "";
            if (pageTitleRaw == "hakkimizda") pageTitle = "Hakkımızda";
            if (pageTitleRaw == "iletisim") pageTitle = "İletişim";
            if (pageTitleRaw == "politikalarimiz") pageTitle = "Politikalarımız";
            return pageTitle;
        }   

        public async Task<Page?> GetPageAsync(string pageTitleRaw)
        {
            var pageTitle = PageTitleReplace(pageTitleRaw);

            var getPage = await unitOfWorkk.GetRepository<Page>().GetAsync(x=>x.PageTitle == pageTitle);

            return getPage;
           
        } 


        public string NormalizeContent(string content)
        {

            var content_ = content.Replace("<script>","").Replace("<style>","")
                .Replace("</script>","").Replace("</style>","").Replace("<link ","").Replace("<iframe>","").Replace("</iframe>","").Replace("<meta","");

            return content_;
        }
      


        public async Task<Page?> UpdatePageAsync(PageUpdateModel page)
        {
            var userEmail = user.GetLoggedInEmail();
            var getPage = await unitOfWorkk.GetRepository<Page>().GetAsync(x => x.Id == page.Id);
            if (getPage != null)
            {
                string pagecontent = NormalizeContent(page.PageContent);
                getPage.PageContent = pagecontent;
                getPage.ModifiedBy = userEmail;
                getPage.ModifiedDate = DateTime.Now;

                await unitOfWorkk.GetRepository<Page>().UpdateAsync(getPage);
                await unitOfWorkk.SaveAsync();
            }

            return getPage;
        }
        






    }
}
