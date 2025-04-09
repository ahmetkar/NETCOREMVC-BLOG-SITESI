using Blog.Entity.DTOs.Page;
using Blog.Entity.DTOs.SiteSettings;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface ISettingService
    {
        Task<Guid?> UpdateSettings(SiteSettingsUpdateModel updateSetting);
        Task<SiteSettings?> GetSettings();
        Task<Page?> GetPageAsync(string pageTitleRaw);
        Task<Page?> UpdatePageAsync(PageUpdateModel page);
        
    }
}
