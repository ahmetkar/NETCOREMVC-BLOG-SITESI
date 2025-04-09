using AutoMapper;
using Blog.Entity.DTOs.Article;
using Blog.Entity.DTOs.Page;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Pages
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<PageUpdateModel, Page>().ReverseMap();
           
        }
    }
}
