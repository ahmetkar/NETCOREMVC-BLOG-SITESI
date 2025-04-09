using AutoMapper;
using Blog.Entity.DTOs.Article;
using Blog.Entity.DTOs.SiteSettings;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.SiteSettings
{
    public class SiteSettingsProfile : Profile
    {
        public SiteSettingsProfile()
        {
            
            CreateMap<SiteSettingsUpdateModel, Blog.Entity.Entities.SiteSettings>().ReverseMap();
        }
    }
}
